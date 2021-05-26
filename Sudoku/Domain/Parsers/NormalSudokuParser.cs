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
            var rowQuadrantsCount = rowLength / quadrantWidth;
            //
            // var verticalQuadrantsCount = rowLength / quadrantWidth;
            // var horizontalQuadrantsCount = rowLength / quadrantHeight;
            // var totalQuadrants = (rowLength / quadrantWidth)
            // Begin index 0 
            // Je weet de spring factor -> quadrantWidth
            // Pak de cellen tot een quadrantWidth (nu ben je bij de buur quandrant)

            var quadrantCounter = 0;
            var minX = 0;
            var minY = 0;
            var maxX = quadrantWidth;
            var maxY = quadrantHeight;
            
            for (var i = 0; i < rowLength; i++)
            {
                minX = maxX - quadrantWidth;
                minY = maxY - quadrantHeight;

                var quadrantCells = 
                    from cell in cells
                    where cell.Coordinate.X >= minX
                    where cell.Coordinate.X < maxX
                    where cell.Coordinate.Y >= minY
                    where cell.Coordinate.Y < maxY
                    select cell;
                    
                // var quadrantCells = cells.FindAll(c =>
                //     c.Coordinate.X >= minX && c.Coordinate.X <= maxX && c.Coordinate.Y >= minY &&
                //     c.Coordinate.Y <= maxY).ToList();
                quadrants.Add(new Quadrant(quadrantCells.ToList()));
                
                quadrantCounter++;

                if (quadrantCounter == rowQuadrantsCount)
                {
                    maxX = quadrantWidth;
                    maxY += quadrantHeight;
                    quadrantCounter = 0;
                }
                else
                {
                    maxX += quadrantWidth;
                }
            }
            
            return new Grid(quadrants);
        }
    }
}