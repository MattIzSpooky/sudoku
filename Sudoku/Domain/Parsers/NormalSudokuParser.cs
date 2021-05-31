using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board;

namespace Sudoku.Domain.Parsers
{
    public class NormalSudokuParser : ISudokuParser
    {
        public virtual Grid[] Parse(string content)
        {
            var squareValue = (int) Math.Round(Math.Sqrt(content.Trim().Length));

            var cells = CreateCells(content, squareValue);
            var quadrants = ComposeQuadrants(cells, squareValue);

            return new Grid[] {new(quadrants)};
        }

        protected List<Cell> CreateCells(string content, int squareValue)
        {
            var cells = new List<Cell>();
            var counter = content.Length;

            for (var y = 0; y < squareValue; y++)
            {
                for (var x = 0; x < squareValue; x++)
                {
                    var index = content.Length - counter;

                    cells.Add(new Cell(new Coordinate(x, y),
                        (int) char.GetNumericValue(content[index..].First())));

                    counter--;
                }
            }

            return cells;
        }

        protected List<Quadrant> ComposeQuadrants(List<Cell> cells, int squareValue)
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

        private List<Quadrant> CreateQuadrants(List<Cell> cells, BoardValues boardValues)
        {
            var quadrants = new List<Quadrant>();

            var quadrantCounter = 0;
            var maxX = boardValues.QuadrantWidth;
            var maxY = boardValues.QuadrantHeight;
            
            for (var i = 0; i < boardValues.SquareValue; i++)
            {
                var minX = maxX - boardValues.QuadrantWidth;
                var minY = maxY - boardValues.QuadrantHeight;

                quadrants.Add(new Quadrant(GetSpecifiedQuadrantCells(cells, minX, maxX, minY, maxY)));

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

        private List<Cell> GetSpecifiedQuadrantCells(IEnumerable<Cell> cells, int minX, int maxX, int minY, int maxY)
        {
            return
                (from cell in cells
                    where cell.Coordinate.X >= minX
                    where cell.Coordinate.X < maxX
                    where cell.Coordinate.Y >= minY
                    where cell.Coordinate.Y < maxY
                    select cell).ToList();
        }

        private struct BoardValues
        {
            public int SquareValue { get; set; }
            public int QuadrantWidth { get; set; }
            public int QuadrantHeight { get; set; }
            public int RowQuadrantsCount { get; set; }
        }
    }
}