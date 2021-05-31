using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board;

namespace Sudoku.Domain.Parsers
{
    public class SamuraiSudokuParser : NormalSudokuParser
    {
        private const int AmountOfSubSudokus = 5;

        public override Grid[] Parse(string content)
        {
            // TODO: Create cells like normal 9x9
            // Create 5 "sub" Sudoku's, work with a min X and max Y for the sub Sudoku's
            // Each row = 81 characters = 1 sudoku
            // Add an offset for each sub sudoku
            // Combine them

            var cleanedContent = content.Replace(Environment.NewLine, string.Empty).Trim();
            var amountOfCellsPerSubSudoku = cleanedContent.Length / AmountOfSubSudokus;
            var squareValue = (int) Math.Round(Math.Sqrt(amountOfCellsPerSubSudoku));

            var subSudokuCells = Enumerable
                .Range(0, AmountOfSubSudokus)
                .Select(i => cleanedContent.Substring(i * amountOfCellsPerSubSudoku, amountOfCellsPerSubSudoku))
                //.Select(subSudokuContent => CreateCells(subSudokuContent, squareValue))
                .ToArray();

            // var quadrants = new List<Quadrant>();
            // foreach (var cells in subSudokuCells) 
            //     quadrants.AddRange(ComposeQuadrants(cells, squareValue));


            // .Select(cells => new Grid(ComposeQuadrants(cells, squareValue)))


            // TODO: Add the overlapping of cells

            throw new NotImplementedException();
        }
    }
}