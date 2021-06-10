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

            var sudokuComponents = CreateCells(content, squareValue,offsetX, offsetY);
            var quadrants = ComposeQuadrants(sudokuComponents, squareValue, offsetX, offsetY);

            var field = new Field(quadrants)
            {
                SolverStrategy = new BackTrackingSolver(), OffsetX = offsetX, OffsetY = offsetY
            };

            return new[] {field};
        }
        private List<ISudokuComponent> CreateCells(string content, int squareValue, int offsetX, int offsetY)
        {
            var quadrantHeight = (int) Math.Floor(Math.Sqrt(squareValue));
            var quadrantWidth = squareValue / quadrantHeight;
            var rowQuadrantsCount = squareValue / quadrantWidth;
            var cutFactor = squareValue * rowQuadrantsCount;
            
            StringBuilder builder = new StringBuilder();
            for (var i = 1; i <= content.Length; i++)
            {
                builder.Append(content[i - 1]);
                
                if (i != 1 && i % quadrantWidth == 0 && i % squareValue != 0 && i % cutFactor != 0)
                    builder.Append('|');
                
                if (i != 1 && i % cutFactor == 0 && i != content.Length)
                    for (var j = 1; j <= squareValue + rowQuadrantsCount - 1; j++)
                        builder.Append('-');
            }

            var myString = builder.ToString();
            var rowBuilder = new RowBuilder(offsetX, offsetY);
            var sudokuComponents = new List<ISudokuComponent>();
            var counter = myString.Length;

            var rows = Math.Pow(quadrantWidth * quadrantHeight, 2) / (quadrantWidth * quadrantHeight * rowQuadrantsCount) - 1;
            
            var totalY = squareValue + rows;
            var totalX = myString[..myString.IndexOf("-", StringComparison.Ordinal)].Length / rowQuadrantsCount;
            for (var y = 0; y < totalY; y++)
            {
                for (var x = 0; x < totalX; x++)
                {
                    var index = myString.Length - counter;

                    if (myString[index] == '|')
                    {
                        sudokuComponents.Add(new Wall(false){Coordinate = new Coordinate(x + offsetX, y + offsetY)});
                        rowBuilder.BuildWall();
                    }
                    else if (myString[index] == '-')
                    {
                        sudokuComponents.Add(new Wall(true){Coordinate = new Coordinate(x + offsetX, y + offsetY)});
                        rowBuilder.BuildWall(true);
                    }
                    else
                    {
                        var cellValue = (int) char.GetNumericValue(myString[index..].First());
                        var cell = new CellLeaf(new Coordinate(x + offsetX, y + offsetY), cellValue) {IsLocked = cellValue != 0};
                        sudokuComponents.Add(cell);
                        rowBuilder.BuildCell(cell);
                    }

                    counter--;
                }
            }

            return sudokuComponents;
        }

        private List<QuadrantComposite> ComposeQuadrants(List<ISudokuComponent> components, int squareValue, int offsetX, int offsetY)
        {
            var quadrantHeight = (int) Math.Floor(Math.Sqrt(squareValue));
            var quadrantWidth = squareValue / quadrantHeight;
            var rowQuadrantsCount = squareValue / quadrantWidth;

            var boardValues = new BoardValues
            {
                SquareValue = squareValue,
                QuadrantHeight = quadrantHeight + 1,
                QuadrantWidth = quadrantWidth + 1,
                RowQuadrantsCount = rowQuadrantsCount,
                OffsetX = offsetX,
                OffsetY = offsetY
            };

            return CreateQuadrants(components, boardValues);
        }

        private List<QuadrantComposite> CreateQuadrants(List<ISudokuComponent> components, BoardValues boardValues)
        {
            var quadrants = new List<QuadrantComposite>();

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