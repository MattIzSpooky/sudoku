using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board;

namespace Sudoku.Domain.Parsers
{
    public class SamuraiSudokuParser : ISudokuParser
    {
        private const int AmountOfSubSudokus = 5;

        public Grid Parse(string content)
        {
            // TODO: Create cells like normal 9x9
            // Create 5 "sub" Sudoku's, work with a min X and max Y for the sub Sudoku's
            // Each row = 81 characters = 1 sudoku
            // Add an offset for each sub sudoku
            // Combine them

            var cleanedContent  = content.Replace(Environment.NewLine, string.Empty).Trim();
            var amountOfCellsPerSubSudoku = cleanedContent.Length / AmountOfSubSudokus;
            var squareValue = (int) Math.Round(Math.Sqrt(amountOfCellsPerSubSudoku));
            
            var subSudokuCells = Enumerable
                .Range(0, AmountOfSubSudokus)
                .Select(i => cleanedContent.Substring(i * amountOfCellsPerSubSudoku, amountOfCellsPerSubSudoku))
                .ToArray();
            
            var total = new List<Cell>();
            total.AddRange(CreateCells(subSudokuCells[0], squareValue));
            total.AddRange(CreateCells(subSudokuCells[1], squareValue, 12));
            total.AddRange(CreateCells(subSudokuCells[2], squareValue, 6, 6));
            total.AddRange(CreateCells(subSudokuCells[3], squareValue, 0, 12));
            total.AddRange(CreateCells(subSudokuCells[3], squareValue, 12, 12));
    
            
            // TODO: Add the overlapping of cells

            throw new NotImplementedException();
        }

        private List<Cell> CreateCells(string content, int squareValue, int xOffset = 0, int yOffset = 0)
        {
            var cells = new List<Cell>();
            var counter = content.Length;

            for (var y = 0; y < squareValue; y++)
            {
                for (var x = 0; x < squareValue; x++)
                {
                    var index = content.Length - counter;

                    cells.Add(new Cell(new Coordinate(x + xOffset, y + yOffset),
                        (int) char.GetNumericValue(content[index..].First())));

                    counter--;
                }
            }

            return cells;
        }
    }
}