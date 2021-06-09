using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Extensions;
using Sudoku.Domain.Solvers;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board
{
    public class Field : ISudokuComponent
    {
        private readonly List<QuadrantComposite> _quadrants;

        public int OffsetX { get; }
        public int OffsetY { get; }
        public int MaxValue { get; }
        
        public ISolverStrategy SolverStrategy { get; set; }

        public IReadOnlyList<QuadrantComposite> Quadrants => _quadrants;

        public List<CellLeaf> GetOrderedCells()
        {
            return Find(c => !c.IsComposite() && c is CellLeaf)
                .Cast<CellLeaf>()
                .OrderBy(c => c.Coordinate.Y)
                .ThenBy(c => c.Coordinate.X)
                .ToList();
        }

        public IEnumerable<ISudokuComponent> Find(Func<ISudokuComponent, bool> finder)
        {
            return GetChildren().Descendants(i => i.GetChildren()).Where(finder);
        }

        public bool IsComposite() => true;

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISudokuComponent> GetChildren()
        {
            return _quadrants;
        }

        public Grid Accept(ISudokuVisitor visitor)
        {
            return visitor.Visit(this);
        }
        
        
        public Field(List<QuadrantComposite> quadrants, int maxValue, int offsetX = 0, int offsetY = 0)
        {
            _quadrants = quadrants;

            OffsetX = offsetX;
            OffsetY = offsetY;

            MaxValue = maxValue;
        }

        public void Solve() => SolverStrategy.Solve(this);
        
        public bool Validate()
        {
            var cells = GetChildren().SelectMany(q => q.GetChildren()).Cast<CellLeaf>().ToList();
                
            foreach (var cell in cells)
            {
                cell.IsValid = true;
                    
                if (cell.IsLocked || cell.Value.Value == 0) continue;

                var rowColumn = cells
                    .Where(c => (c.Coordinate.Y == cell.Coordinate.Y || c.Coordinate.X == cell.Coordinate.X) &&
                                c != cell)
                    .FirstOrDefault(c => c.Value.Value == cell.Value.Value);

                if (rowColumn != null)
                {
                    cell.IsValid = false;
                    return false;
                }

                var quadrant = Find(c => c.GetChildren().Contains(cell)).First();

                if (quadrant.GetChildren().Cast<CellLeaf>()
                    .FirstOrDefault(c => c.Value.Value == cell.Value.Value && c != cell) != null)
                {
                    cell.IsValid = false;
                    return false;
                }
            }

            return true;
        }
    }
}