using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board;

namespace Sudoku.Domain.Parsers
{
    public class NormalSudokuParser : ISudokuParser
    {
        public Grid Parse(string content)
        {
            var quadrants = new List<Quadrant>();
            var cells = new List<Cell>();

            var squareValue = (int) Math.Round(Math.Sqrt(content.Trim().Length));

            CreateCells(content, squareValue, cells);
            CreateQuadrants(squareValue, quadrants, cells);

            return new Grid(quadrants);
        }

        private void CreateCells(string content, int squareValue, ICollection<Cell> cells)
        {
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
        }

        private void CreateQuadrants(int squareValue, List<Quadrant> quadrants, List<Cell> cells)
        {
            var quadrantHeight = (int) Math.Floor(Math.Sqrt(squareValue));
            var quadrantWidth = squareValue / quadrantHeight;
            var rowQuadrantsCount = squareValue / quadrantWidth;

            var quadrantCounter = 0;
            var minX = 0;
            var minY = 0;
            var maxX = quadrantWidth;
            var maxY = quadrantHeight;

            for (var i = 0; i < squareValue; i++)
            {
                minX = maxX - quadrantWidth;
                minY = maxY - quadrantHeight;

                quadrants.Add(new Quadrant(GetSpecifiedQuadrantCells(cells, minX, maxX, minY, maxY)));

                quadrantCounter++;

                if (quadrantCounter == rowQuadrantsCount)
                {
                    maxX = quadrantWidth;
                    maxY += quadrantHeight;
                    quadrantCounter = 0;
                    
                    continue;
                }

                maxX += quadrantWidth;
            }
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
    }
}