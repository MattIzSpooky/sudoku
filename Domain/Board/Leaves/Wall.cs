using System.Collections.Generic;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board.Leaves
{
    public class Wall : ISudokuComponent
    {
        public bool Horizontal { get; }

        public Wall(bool horizontal)
        {
            Horizontal = horizontal;
        }

        public bool IsComposite() => false;
        public Coordinate Coordinate { get; set; }

        public void Accept(ISudokuComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IEnumerable<ISudokuComponent> GetChildren() => null;
    }
}