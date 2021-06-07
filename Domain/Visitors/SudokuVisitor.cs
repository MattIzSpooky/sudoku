using System;
using System.Linq;
using Sudoku.Domain.Board;
using Sudoku.Domain.Board.Builders;

namespace Sudoku.Domain.Visitors
{
    public class SudokuVisitor : ISudokuVisitor
    {
        public Grid Visit(Board.Sudoku sudoku)
        {
            var gridBuilder = new GridBuilder(sudoku.OffsetX, sudoku.OffsetY);

            
            var cellsInOrder = sudoku.GetOrderedCells();
            var totalWidth = cellsInOrder.Max(cellLeaf => cellLeaf.Coordinate.X) + 1;
            
            var quadrantSize = sudoku.Quadrants[0].Children.Count;
            var nextHorizontal = Convert.ToInt32(Math.Floor(Math.Sqrt(quadrantSize)));
            var nextVertical = Convert.ToInt32(Math.Ceiling(Math.Sqrt(quadrantSize)));

            for (var i = 0; i < cellsInOrder.Count; ++i)
            {
                var leaf = cellsInOrder[i];
                var nextLeaf = i + 1 > cellsInOrder.Count - 1 ? null : cellsInOrder[i + 1];
                var downLeaf = cellsInOrder.FirstOrDefault(cellLeaf => cellLeaf.Coordinate.Y == leaf.Coordinate.Y + 1 && cellLeaf.Coordinate.X == leaf.Coordinate.X);
                var quadrant = sudoku.Quadrants.First(q => q.Children.Contains(leaf));
                
                gridBuilder.BuildCell(leaf);

                if (nextLeaf?.Coordinate.Y == leaf.Coordinate.Y && !quadrant.Children.Contains(nextLeaf!))
                {
                    gridBuilder.BuildWall();
                }

                if (nextLeaf?.Coordinate.Y == leaf.Coordinate.Y) continue;
                
                gridBuilder.BuildRow();
                
                if ((leaf.Coordinate.Y + 1) % nextHorizontal != 0 || downLeaf == null) continue;

                for (var wall = 0; wall < totalWidth; ++wall)
                {
                    gridBuilder.BuildWall(true);
                    if ((wall + 1) % nextVertical == 0) gridBuilder.BuildEmptySpace();
                }
                
                gridBuilder.BuildRow();
            }
            
            var result = gridBuilder.GetResult();
            gridBuilder.Reset();
            return result;
        }
    }
}