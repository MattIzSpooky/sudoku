using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board;
using Sudoku.Domain.Board.Builders;
using Sudoku.Domain.Board.Leaves;
using Sudoku.Domain.Solvers;

namespace Sudoku.Domain.Parsers
{
    public class NormalSudokuParser : ISudokuParser
    {
        public virtual Field[] Parse(string content, int offsetX = 0, int offsetY = 0)
        {
            var squareValue = (int) Math.Round(Math.Sqrt(content.Trim().Length));

            var cells = CreateCells(content, squareValue);
            var quadrants = InsertOtherLeaves(offsetX, offsetY, ComposeQuadrants(cells, squareValue));

            var field = new Field(quadrants)
            {
                SolverStrategy = new BackTrackingSolver(), OffsetX = offsetX, OffsetY = offsetY, MaxValue = squareValue
            };

            return new[] {field};
        }

        private List<CellLeaf> CreateCells(string content, int squareValue)
        {
            var cells = new List<CellLeaf>();
            var counter = content.Length;

            for (var y = 0; y < squareValue; y++)
            {
                for (var x = 0; x < squareValue; x++)
                {
                    var index = content.Length - counter;

                    var cellValue = (int) char.GetNumericValue(content[index..].First());
                    cells.Add(new CellLeaf(new Coordinate(x, y), cellValue) {IsLocked = cellValue != 0});

                    counter--;
                }
            }

            return cells;
        }

        private List<QuadrantComposite> ComposeQuadrants(List<CellLeaf> cells, int squareValue)
        {
            var quadrantHeight = (int) Math.Floor(Math.Sqrt(squareValue));
            var quadrantWidth = squareValue / quadrantHeight;
            var rowQuadrantsCount = squareValue / quadrantWidth;

            var boardValues = new BoardValues
            {
                SquareValue = squareValue,
                QuadrantHeight = quadrantHeight,
                QuadrantWidth = quadrantWidth,
                RowQuadrantsCount = rowQuadrantsCount
            };

            return CreateQuadrants(cells, boardValues);
        }

        private List<QuadrantComposite> CreateQuadrants(List<CellLeaf> cells, BoardValues boardValues)
        {
            var quadrants = new List<QuadrantComposite>();

            var quadrantCounter = 0;
            var maxX = boardValues.QuadrantWidth;
            var maxY = boardValues.QuadrantHeight;

            for (var i = 0; i < boardValues.SquareValue; i++)
            {
                var minX = maxX - boardValues.QuadrantWidth;
                var minY = maxY - boardValues.QuadrantHeight;

                var quadrant = new QuadrantComposite();
                foreach (var cell in GetSpecifiedQuadrantCells(cells, minX, maxX, minY, maxY))
                {
                    quadrant.AddComponent(cell);
                }

                quadrants.Add(quadrant);

                quadrantCounter++;

                if (quadrantCounter == boardValues.RowQuadrantsCount)
                {
                    maxX = boardValues.QuadrantWidth;
                    maxY += boardValues.QuadrantHeight;
                    quadrantCounter = 0;
                    continue;
                }

                maxX += boardValues.QuadrantWidth;
            }

            return quadrants;
        }

        private IEnumerable<CellLeaf> GetSpecifiedQuadrantCells(IEnumerable<CellLeaf> cells, int minX, int maxX,
            int minY,
            int maxY) =>
            cells.Where(cell => cell.Coordinate.X >= minX &&
                                cell.Coordinate.X < maxX &&
                                cell.Coordinate.Y >= minY &&
                                cell.Coordinate.Y < maxY).ToList();

        private List<QuadrantComposite> InsertOtherLeaves(int offsetX, int offsetY, IReadOnlyList<QuadrantComposite> quadrants)
        {
            var rows = new List<Row>();
            var rowBuilder = new RowBuilder(offsetX, offsetY);

            var cellsInOrder = quadrants
                .SelectMany(q => q.Children.OfType<CellLeaf>())
                .OrderBy(c => c.Coordinate.Y)
                .ThenBy(c => c.Coordinate.X).ToList();

            var totalWidth = cellsInOrder.Max(cellLeaf => cellLeaf.Coordinate.X) + 1;

            var quadrantSize = quadrants[0].Children.Count;
            var nextHorizontal = Convert.ToInt32(Math.Floor(Math.Sqrt(quadrantSize)));
            var nextVertical = Convert.ToInt32(Math.Ceiling(Math.Sqrt(quadrantSize)));

            for (var i = 0; i < cellsInOrder.Count; ++i)
            {
                if (i > 0 && i % (quadrantSize) == 0)
                {
                    rows.Add(rowBuilder.GetResult());
                    rowBuilder.Reset();
                }

                var leaf = cellsInOrder[i];
                var nextLeaf = i + 1 > cellsInOrder.Count - 1 ? null : cellsInOrder[i + 1];
                var downLeaf = cellsInOrder.FirstOrDefault(cellLeaf => cellLeaf.Coordinate.Y == leaf.Coordinate.Y + 1 
                                                                       && cellLeaf.Coordinate.X == leaf.Coordinate.X);
                var quadrant = quadrants.First(q => q.Children.Contains(leaf));

                rowBuilder.BuildCell(leaf);

                if (nextLeaf?.Coordinate.Y == leaf.Coordinate.Y && !quadrant.Children.Contains(nextLeaf))
                {
                    rowBuilder.BuildWall();
                }

                if (nextLeaf?.Coordinate.Y == leaf.Coordinate.Y) continue;

                rowBuilder.InsertRow();

                if ((leaf.Coordinate.Y + 1) % nextHorizontal != 0 || downLeaf == null) continue;

                for (var wall = 0; wall < totalWidth; ++wall)
                {
                    rowBuilder.BuildWall(true);
                    if ((wall + 1) % nextVertical == 0) rowBuilder.BuildEmptySpace();
                }

                rowBuilder.InsertRow();
                
                rows.Add(rowBuilder.GetResult());
                rowBuilder.Reset();
            }

            var newQuadrants = new List<QuadrantComposite>();

            var allItems = rows.SelectMany(r => r.Components).ToList();
            
            return newQuadrants;
        }

        private struct BoardValues
        {
            public int SquareValue { get; set; }
            public int QuadrantWidth { get; set; }
            public int QuadrantHeight { get; set; }
            public int RowQuadrantsCount { get; set; }
        }
    }
}