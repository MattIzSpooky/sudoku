using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board.Leaves;
using Sudoku.Domain.Extensions;
using Sudoku.Domain.Solvers;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board
{
    public class Field
    {
        private readonly List<QuadrantComposite> _quadrants;

        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        
        public ISolverStrategy SolverStrategy { get; set; }

        public IReadOnlyList<QuadrantComposite> Quadrants => _quadrants;

        public Coordinate GetMaxCoordinates()
        {
            var allChildren = _quadrants.SelectMany(q => q.Children).ToList();
            var maxX = allChildren.Max(c => c.Coordinate.X);
            var maxY = allChildren.Max(c => c.Coordinate.Y);

            return new Coordinate(maxX, maxY);
        }

        public int GetMaxValue() => _quadrants[0].Children.OfType<CellLeaf>().Count();
        
        public Field(List<QuadrantComposite> quadrants)
        {
            _quadrants = quadrants;
        }

        public List<CellLeaf> GetOrderedCells()
        {
            return Find(c => !c.IsComposite() && c is CellLeaf)
                .Cast<CellLeaf>()
                .OrderBy(c => c.Coordinate.Y)
                .ThenBy(c => c.Coordinate.X)
                .ToList();
        }

        private IEnumerable<ISudokuComponent> Find(Func<ISudokuComponent, bool> finder)
        {
            return GetChildren().Descendants(i => i.GetChildren()).Where(finder);
        }

        public IEnumerable<ISudokuComponent> GetChildren()
        {
            return _quadrants;
        }

        public void Accept(ISudokuComponentVisitor visitor)
        {
            foreach (var quadrant in _quadrants)
            {
                quadrant.Accept(visitor);
            }
        }

        public void Solve() => SolverStrategy.Solve(this);
        
        public bool Validate()
        {
            // TODO: Solve with visitor maybe?
            var cells = GetChildren().SelectMany(q => q.GetChildren().OfType<CellLeaf>()).ToList();
                
            foreach (var cell in cells)
            {
                cell.IsValid = true;
                    
                if (cell.IsLocked || cell.Value.DefinitiveValue == 0) continue;

                var rowColumn = cells
                    .Where(c => (c.Coordinate.Y == cell.Coordinate.Y || c.Coordinate.X == cell.Coordinate.X) &&
                                c != cell)
                    .FirstOrDefault(c => c.Value.DefinitiveValue == cell.Value.DefinitiveValue);

                if (rowColumn != null)
                {
                    cell.IsValid = false;
                    return false;
                }

                var quadrant = Find(c => c.GetChildren().Contains(cell)).First();

                if (quadrant.GetChildren().OfType<CellLeaf>()
                    .FirstOrDefault(c => c.Value.DefinitiveValue == cell.Value.DefinitiveValue && c != cell) != null)
                {
                    cell.IsValid = false;
                    return false;
                }
            }

            return true;
        }
    }
}