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
            
            MergeOverflowingCells(grids[0].Quadrants[8].Cells, grids[2].Quadrants[0].Cells);
            MergeOverflowingCells(grids[1].Quadrants[6].Cells, grids[2].Quadrants[2].Cells);
            MergeOverflowingCells(grids[3].Quadrants[2].Cells, grids[2].Quadrants[6].Cells);
            MergeOverflowingCells(grids[4].Quadrants[0].Cells, grids[2].Quadrants[8].Cells);
         
            return grids;
        }

        private void MergeOverflowingCells(IReadOnlyList<Cell> firstCells, IReadOnlyList<Cell> secondCells)
        {
            for (var i = 0; i < firstCells.Count; i++)
                firstCells[i].Value = secondCells[i].Value;
        }
    }
}