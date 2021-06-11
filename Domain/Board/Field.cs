using System.Collections.Generic;
using System.Linq;
using Domain.Board.Leaves;
using Domain.Solvers;

namespace Domain.Board
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
            return _quadrants.SelectMany(q => q.Cells)
                .OrderBy(c => c.Coordinate.Y)
                .ThenBy(c => c.Coordinate.X);
        }

        public void Solve() => SolverStrategy?.Solve(this);

        public bool IsValid() => _quadrants.All(q => q.IsValid());
        
        public void Validate()
        {
            var cells = _quadrants.SelectMany(q => q.Cells).ToList();
                
            foreach (var cell in cells)
            {
                cell.IsValid = true;

                if (cell.IsLocked || cell.Value.DefinitiveValue == 0) continue;

                ValidateRowColumn(cells, cell);

                _quadrants.First(q => q.BelongsTo(cell)).Validate();
            }
        }

        private static void ValidateRowColumn(IEnumerable<CellLeaf> cells, CellLeaf cell)
        {
            var rowColumn = cells
                .Where(c => (c.Coordinate.Y == cell.Coordinate.Y || c.Coordinate.X == cell.Coordinate.X) && c != cell)
                .FirstOrDefault(c => c.Value.DefinitiveValue == cell.Value.DefinitiveValue);

            if (rowColumn == null) return;
            
            cell.IsValid = false;
        }
    }
}