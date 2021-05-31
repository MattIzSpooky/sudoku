using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board;

namespace Sudoku.Domain.Parsers
{
    public class SamuraiSudokuParser : NormalSudokuParser
    {
        private const int AmountOfSubSudoku = 5;

        public override Grid[] Parse(string content)
        {
            var cleanedContent = content.Replace(Environment.NewLine, string.Empty).Trim();
            var amountOfCellsPerSubSudoku = cleanedContent.Length / AmountOfSubSudoku;

            var grids = Enumerable
                .Range(0, AmountOfSubSudoku)
                .Select(i => cleanedContent.Substring(i * amountOfCellsPerSubSudoku, amountOfCellsPerSubSudoku))
                .SelectMany(subSudokuContent => base.Parse(subSudokuContent))
                .ToArray();

            // TODO: Add the overlapping of cells
            
            // Extract all the overflowed quadrants
            // 
            

            return grids;
        }
    }
}