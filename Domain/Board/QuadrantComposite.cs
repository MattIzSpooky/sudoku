using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sudoku.Domain.Board.Leaves;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board
{
    public class QuadrantComposite : ISudokuComponent
    {
        private readonly List<ISudokuComponent> _children = new();
        public IReadOnlyList<ISudokuComponent> Children => _children;

        public IReadOnlyList<CellLeaf> Cells => GetChildren().OfType<CellLeaf>().ToImmutableList();

        public void AddComponent(ISudokuComponent component)
        {
            _children.Add(component);
        }

        public void Accept(ISudokuComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IEnumerable<ISudokuComponent> GetChildren() => Children;

        public bool IsValid() => Cells.All(c => c.IsValid);

        public bool IsComposite() => true;
        public Coordinate Coordinate { get; set; }

        public CellLeaf? CellByCoordinate(Coordinate coordinate) =>
            Cells.FirstOrDefault(g => g.Coordinate.X == coordinate.X && g.Coordinate.Y == coordinate.Y);

        public bool BelongsTo(ISudokuComponent component) => Children.Any(c => c == component);
        
        public void Validate()
        {
            var duplicates = Cells
                .Where(c => c.Value.DefinitiveValue != 0)
                .GroupBy(c => c.Value.DefinitiveValue)
                .Where(g => g.Count() > 1)
                .SelectMany(y => y)
                .Where(cell => !cell.IsLocked);

            foreach (var cell in duplicates)
            {
                cell.IsValid = false;
            }
        }
    }
}