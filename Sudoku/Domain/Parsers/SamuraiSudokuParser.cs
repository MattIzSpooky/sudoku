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


            MergeOverflowingQuadrants(grids, amountOfCellsPerSubSudoku);

            return grids;
        }

        private void MergeOverflowingQuadrants(IReadOnlyList<Grid> grids, int amountOfCellsPerSubSudoku)
        {
            const int quadrantFactor = 2;
            
            var centerQuadrant = 0;
            var centerGrid = grids.Count / quadrantFactor;
            var leftQuadrant = (int) Math.Sqrt(amountOfCellsPerSubSudoku) - 1;
            
            for (var i = 0; i < grids.Count; i++)
            {
                if (i != centerGrid)
                {
                    var firstQuadrant = grids[i].Quadrants[leftQuadrant].Cells;
                    var secondQuadrant = grids[centerGrid].Quadrants[centerQuadrant].Cells;

                    MergeOverflowingCells(firstQuadrant, secondQuadrant);
                }


                leftQuadrant -= quadrantFactor;
                centerQuadrant += quadrantFactor;
            }
        }

        private static void MergeOverflowingCells(IReadOnlyList<Cell> firstCells, IReadOnlyList<Cell> secondCells)
        {
            for (var i = 0; i < firstCells.Count; i++)
                firstCells[i].Value = secondCells[i].Value;
        }
    }
}