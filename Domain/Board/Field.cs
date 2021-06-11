using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board.Leaves;
using Sudoku.Domain.Extensions;
using Sudoku.Domain.Solvers;

namespace Sudoku.Domain.Board
{
    public class Field
    {
        private readonly List<QuadrantComposite> _quadrants;
        public ISolverStrategy? SolverStrategy { get; set; }

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

        public IEnumerable<CellLeaf> GetOrderedCells()
        {
            return Find(c => !c.IsComposite() && c is CellLeaf)
                .Cast<CellLeaf>()
                .OrderBy(c => c.Coordinate.Y)
                .ThenBy(c => c.Coordinate.X);
        }

        private IEnumerable<ISudokuComponent> Find(Func<ISudokuComponent, bool> finder) => _quadrants
            .Descendants<ISudokuComponent>(i => i.GetChildren()).Where(finder);

        public void Solve() => SolverStrategy?.Solve(this);
        
        public bool Validate()
        {
            var cells = _quadrants.SelectMany(q => q.Cells).ToList();
                
            foreach (var cell in cells)
            {
                cell.IsValid = true;
                    
                if (cell.IsLocked || cell.Value.DefinitiveValue == 0) continue;

                if (ValidateRowColumn(cells, cell) != null) return false;

                var quadrant = Find(c => c.GetChildren().Contains(cell)).Cast<QuadrantComposite>().First();

                if (quadrant.Validate(cell)) continue;

                cell.IsValid = false;
                return false;
            }

            return true;
        }

        private static CellLeaf? ValidateRowColumn(IEnumerable<CellLeaf> cells, CellLeaf cell)
        {
            var rowColumn = cells
                .Where(c => (c.Coordinate.Y == cell.Coordinate.Y || c.Coordinate.X == cell.Coordinate.X) &&
                            c != cell)
                .FirstOrDefault(c => c.Value.DefinitiveValue == cell.Value.DefinitiveValue);

            if (rowColumn == null) return null;
            
            cell.IsValid = false;
            return cell;
        }
    }
}