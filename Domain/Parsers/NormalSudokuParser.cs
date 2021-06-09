using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board;
using Sudoku.Domain.Solvers;

namespace Sudoku.Domain.Parsers
{
    public class NormalSudokuParser : ISudokuParser
    {
        public virtual Field[] Parse(string content, int offsetX = 0, int offsetY = 0)
        {
            var squareValue = (int) Math.Round(Math.Sqrt(content.Trim().Length));

            var cells = CreateCells(content, squareValue);
            var quadrants = ComposeQuadrants(cells, squareValue);

            var field =  new Field(quadrants, squareValue, offsetX, offsetY) {SolverStrategy = new BackTrackingSolver()};
            
            return new[] {field};
        }

        private List<CellLeaf> CreateCells(string content, int squareValue)
        {
            var cells = new List<CellLeaf>();
            var counter = content.Length;

            for (var y = 0; y < squareValue; y++)
            {
                for (var x = 0; x < squareValue; x++)
                {
                    var index = content.Length - counter;

                    var cellValue = (int) char.GetNumericValue(content[index..].First());
                    var isLocked = cellValue != 0;
                    cells.Add(new CellLeaf(new Coordinate(x, y), cellValue, isLocked));

                    counter--;
                }
            }

            return cells;
        }

        private List<QuadrantComposite> ComposeQuadrants(List<CellLeaf> cells, int squareValue)
        {
            var quadrantHeight = (int) Math.Floor(Math.Sqrt(squareValue));
            var quadrantWidth = squareValue / quadrantHeight;
            var rowQuadrantsCount = squareValue / quadrantWidth;

            var boardValues = new BoardValues
            {
                SquareValue = squareValue,
                QuadrantHeight = quadrantHeight,
                QuadrantWidth = quadrantWidth,
                RowQuadrantsCount = rowQuadrantsCount
            };

            return CreateQuadrants(cells, boardValues);
        }

        private List<QuadrantComposite> CreateQuadrants(List<CellLeaf> cells, BoardValues boardValues)
        {
            var quadrants = new List<QuadrantComposite>();

            var quadrantCounter = 0;
            var maxX = boardValues.QuadrantWidth;
            var maxY = boardValues.QuadrantHeight;

            for (var i = 0; i < boardValues.SquareValue; i++)
            {
                var minX = maxX - boardValues.QuadrantWidth;
                var minY = maxY - boardValues.QuadrantHeight;

                quadrants.Add(new QuadrantComposite(GetSpecifiedQuadrantCells(cells, minX, maxX, minY, maxY)));

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

        private List<CellLeaf> GetSpecifiedQuadrantCells(IEnumerable<CellLeaf> cells, int minX, int maxX, int minY, int maxY) =>
            cells.Where(cell => cell.Coordinate.X >= minX &&
                                cell.Coordinate.X < maxX &&
                                cell.Coordinate.Y >= minY &&
                                cell.Coordinate.Y < maxY).ToList();
        
        private struct BoardValues
        {
            public int SquareValue { get; set; }
            public int QuadrantWidth { get; set; }
            public int QuadrantHeight { get; set; }
            public int RowQuadrantsCount { get; set; }
        }
    }
}