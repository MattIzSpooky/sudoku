using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board;

namespace Sudoku.Domain.Parsers
{
    public class NormalSudokuParser : ISudokuParser
    {
        private readonly List<Quadrant> _quadrants = new();
        private readonly List<Cell> _cells = new();

        public Grid Parse(string content)
        {
            var squareValue = (int) Math.Round(Math.Sqrt(content.Trim().Length));

            CreateCells(content, squareValue);
            ComposeQuadrants(squareValue);

            return new Grid(_quadrants);
        }

        private void CreateCells(string content, int squareValue)
        {
            var counter = content.Length;

            for (var y = 0; y < squareValue; y++)
            {
                for (var x = 0; x < squareValue; x++)
                {
                    var index = content.Length - counter;

                    _cells.Add(new Cell(new Coordinate(x, y),
                        (int) char.GetNumericValue(content[index..].First())));

                    counter--;
                }
            }
        }

        private void ComposeQuadrants(int squareValue)
        {
            var quadrantHeight = (int) Math.Floor(Math.Sqrt(squareValue));
            var quadrantWidth = squareValue / quadrantHeight;
            var rowQuadrantsCount = squareValue / quadrantWidth;

            CreateQuadrants(squareValue, quadrantWidth, quadrantHeight, rowQuadrantsCount);
        }

        private void CreateQuadrants(int squareValue, int quadrantWidth, int quadrantHeight, int rowQuadrantsCount)
        {
            var quadrantCounter = 0;
            var maxX = quadrantWidth;
            var maxY = quadrantHeight;

            for (var i = 0; i < squareValue; i++)
            {
                var minX = maxX - quadrantWidth;
                var minY = maxY - quadrantHeight;

                _quadrants.Add(new Quadrant(GetSpecifiedQuadrantCells(_cells, minX, maxX, minY, maxY)));

                quadrantCounter++;

                if (CheckRowOverflow(
                    quadrantWidth, 
                    quadrantHeight, 
                    rowQuadrantsCount,
                    ref quadrantCounter, ref maxX, ref maxY)
                )
                    continue;

                maxX += quadrantWidth;
            }
        }

        private bool CheckRowOverflow(
            int quadrantWidth, 
            int quadrantHeight, 
            int rowQuadrantsCount,
            ref int quadrantCounter, ref int maxX, ref int maxY)
        {
            if (quadrantCounter != rowQuadrantsCount)
                return false;

            maxX = quadrantWidth;
            maxY += quadrantHeight;
            quadrantCounter = 0;

            return true;
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