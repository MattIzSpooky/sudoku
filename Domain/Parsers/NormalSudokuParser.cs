using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Domain.Board;
using Sudoku.Domain.Board.Builders;
using Sudoku.Domain.Board.Leaves;
using Sudoku.Domain.Solvers;

namespace Sudoku.Domain.Parsers
{
    public class NormalSudokuParser : ISudokuParser
    {
        public virtual Field[] Parse(string content, int offsetX = 0, int offsetY = 0)
        {
            var squareValue = (int) Math.Round(Math.Sqrt(content.Trim().Length));
            var quadrantHeight = (int) Math.Floor(Math.Sqrt(squareValue));
            var quadrantWidth = squareValue / quadrantHeight;
           
            var boardValues = new BoardValues
            {
                SquareValue = squareValue,
                QuadrantHeight = quadrantHeight,
                QuadrantWidth = quadrantWidth,
                RowQuadrantsCount = squareValue / quadrantWidth,
                OffsetX = offsetX,
                OffsetY = offsetY
            };

            var sudokuComponents = CreateCells(content, boardValues);
            var quadrants = CreateQuadrants(sudokuComponents, boardValues);

            var field = new Field(quadrants)
            {
                SolverStrategy = new BackTrackingSolver(), OffsetX = offsetX, OffsetY = offsetY
            };

            return new[] {field};
        }
        private static List<ISudokuComponent> CreateCells(string content, BoardValues boardValues)
        {
            var sudokuComponents = new List<ISudokuComponent>();
            
            var newContent = BuildContentWithWalls(content, boardValues);
            var rowBuilder = new RowBuilder(boardValues.OffsetX, boardValues.OffsetY);
            
            var rows = Math.Pow(boardValues.QuadrantWidth * boardValues.QuadrantHeight, 2) / 
                (boardValues.QuadrantWidth * boardValues.QuadrantHeight * boardValues.RowQuadrantsCount) - 1;
            
            var counter = newContent.Length;
            var totalY = boardValues.SquareValue + rows;
            var totalX = newContent[..newContent.IndexOf("-", StringComparison.Ordinal)].Length / 
                         boardValues.RowQuadrantsCount;
            for (var y = 0; y < totalY; y++)
            {
                for (var x = 0; x < totalX; x++)
                {
                    var index = newContent.Length - counter;

                    if (newContent[index] == '|')
                    {
                        sudokuComponents.Add(new Wall(false){Coordinate = new Coordinate(x + boardValues.OffsetX, y + boardValues.OffsetY)});
                        rowBuilder.BuildWall();
                    }
                    else if (newContent[index] == '-')
                    {
                        sudokuComponents.Add(new Wall(true){Coordinate = new Coordinate(x + boardValues.OffsetX, y + boardValues.OffsetY)});
                        rowBuilder.BuildWall(true);
                    }
                    else
                    {
                        var cellValue = (int) char.GetNumericValue(newContent[index..].First());
                        var cell = new CellLeaf(new Coordinate(x + boardValues.OffsetX, y + boardValues.OffsetY), cellValue) {IsLocked = cellValue != 0};
                        sudokuComponents.Add(cell);
                        rowBuilder.BuildCell(cell);
                    }

                    counter--;
                }
            }

            return sudokuComponents;
        }

        private static string BuildContentWithWalls(string content, BoardValues boardValues)
        {
            var cutFactor = boardValues.SquareValue * boardValues.RowQuadrantsCount;
            
            StringBuilder builder = new();
            for (var i = 1; i <= content.Length; i++)
            {
                builder.Append(content[i - 1]);

                if (i != 1 && i % boardValues.QuadrantWidth == 0 && i % boardValues.SquareValue != 0 && i % cutFactor != 0)
                    builder.Append('|');

                if (i != 1 && i % cutFactor == 0 && i != content.Length)
                    for (var j = 1; j <= boardValues.SquareValue + boardValues.RowQuadrantsCount - 1; j++)
                        builder.Append('-');
            }
            
            return builder.ToString();
        }

        private List<QuadrantComposite> CreateQuadrants(List<ISudokuComponent> components, BoardValues boardValues)
        {
            var quadrants = new List<QuadrantComposite>();
            
            boardValues.QuadrantWidth += 1;
            boardValues.QuadrantHeight += 1;

            var quadrantCounter = 0;
            var maxX = boardValues.QuadrantWidth;
            var maxY = boardValues.QuadrantHeight;

            for (var i = 0; i < boardValues.SquareValue; i++)
            {
                var minX = maxX - boardValues.QuadrantWidth;
                var minY = maxY - boardValues.QuadrantHeight;

                var quadrant = new QuadrantComposite();
                foreach (var cell in GetSpecifiedQuadrantCells(
                    components, 
                    minX + boardValues.OffsetX, 
                    maxX + boardValues.OffsetX - 1, 
                    minY + boardValues.OffsetY, 
                    maxY + boardValues.OffsetY - 1))
                {
                    quadrant.AddComponent(cell);
                }

                quadrants.Add(quadrant);

                quadrantCounter++;

                if (quadrantCounter == boardValues.RowQuadrantsCount)
                {
                    maxX = boardValues.QuadrantWidth;
                    maxY += boardValues.QuadrantHeight;
                    quadrantCounter = 0;
                    continue;
                }

                maxX += boardValues.QuadrantWidth;
            }

            return quadrants;
        }

        private IEnumerable<ISudokuComponent> GetSpecifiedQuadrantCells(IEnumerable<ISudokuComponent> components, 
            int minX, 
            int maxX,
            int minY,
            int maxY) =>
            components.Where(cell => 
                cell.Coordinate.X >= minX &&
                cell.Coordinate.X <= maxX &&
                cell.Coordinate.Y >= minY &&
                cell.Coordinate.Y <= maxY)
                .ToList();

        private struct BoardValues
        {
            public int SquareValue { get; set; }
            public int QuadrantWidth { get; set; }
            public int QuadrantHeight { get; set; }
            public int RowQuadrantsCount { get; set; }
            public int OffsetX { get; set; }
            public int OffsetY { get; set; }
        }
    }
}