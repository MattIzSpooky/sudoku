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

        public bool IsComposite() => true;
        public Coordinate Coordinate { get; set; }

        public CellLeaf? CellByCoordinate(Coordinate coordinate) =>
            Cells.FirstOrDefault(g => g.Coordinate.X == coordinate.X && g.Coordinate.Y == coordinate.Y);

        public bool Validate(CellLeaf cell) => Cells
            .FirstOrDefault(c => c.Value.DefinitiveValue == cell.Value.DefinitiveValue && c != cell) == null;
    }
}