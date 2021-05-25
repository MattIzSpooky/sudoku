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
            var rowLength = (int) Math.Round(Math.Sqrt(content.Trim().Length));
            
            var quadrants = new List<Quadrant>();
            var cells = new List<Cell>();

            var counter = content.Length;
            for (var y = 0; y < rowLength; y++)
            {
                for (var x = 0; x < rowLength; x++)
                {
                    var index = content.Length - counter;
                    cells.Add(new Cell(new Coordinate(x,y), 
                        (int) char.GetNumericValue(content.Substring(index).First())));

                    counter--;
                }
            }


            var quadrantHeight = (int) Math.Floor(Math.Sqrt(rowLength));
            var quadrantWidth = rowLength / quadrantHeight;

            foreach (var cell in cells)
            {
                
            }

            quadrants.Add(new Quadrant(cells));
            return new Grid(quadrants);
        }
    }
}