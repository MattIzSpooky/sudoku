using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sudoku.Domain.Board;
using Sudoku.Domain.Board.Leaves;

namespace Sudoku.Domain.Parsers
{
    public class SamuraiSudokuParser : NormalSudokuParser
    {
        private const int AmountOfSubSudoku = 5;
        
        private static readonly int[] XOffsets = {
            0, 16, 8, 0, 16
        };

        private static readonly int[] YOffsets = {
            0, 0, 8, 16, 16
        };

        public override Field[] Parse(string content, int offsetX = 0, int offsetY = 0)
        {
            var cleanedContent = content.Replace(Environment.NewLine, string.Empty).Trim();
            var amountOfCellsPerSubSudoku = cleanedContent.Length / AmountOfSubSudoku;

            var grids = Enumerable
                .Range(0, AmountOfSubSudoku)
                .Select(i => cleanedContent.Substring(i * amountOfCellsPerSubSudoku, amountOfCellsPerSubSudoku))
                .SelectMany((subSudokuContent, i) => base.Parse(subSudokuContent, XOffsets[i], YOffsets[i]))
                .ToArray();


            MergeOverflowingQuadrants(grids, amountOfCellsPerSubSudoku);

            return grids;
        }

        private static void MergeOverflowingQuadrants(IReadOnlyList<Field> grids, int amountOfCellsPerSubSudoku)
        {
            const int quadrantFactor = 2;
            
            var centerQuadrant = 0;
            var centerGrid = grids.Count / quadrantFactor;
            var leftQuadrant = (int) Math.Sqrt(amountOfCellsPerSubSudoku) - 1;
            
            for (var i = 0; i < grids.Count; i++)
            {
                if (i != centerGrid)
                {
                    var firstQuadrant = grids[i].Quadrants[leftQuadrant].Children.OfType<CellLeaf>().ToImmutableList();
                    var secondQuadrant = grids[centerGrid].Quadrants[centerQuadrant].Children.OfType<CellLeaf>().ToImmutableList();

                    MergeOverflowingCells(firstQuadrant, secondQuadrant);
                }


                leftQuadrant -= quadrantFactor;
                centerQuadrant += quadrantFactor;
            }
        }

        private static void MergeOverflowingCells(IReadOnlyList<CellLeaf> firstCells, IReadOnlyList<CellLeaf> secondCells)
        {
            for (var i = 0; i < firstCells.Count; i++)
                firstCells[i].Value = secondCells[i].Value;
        }
    }
}