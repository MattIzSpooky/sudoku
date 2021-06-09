using System.Collections.Generic;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board.Leaves
{
    public class EmptySpace : ISudokuComponent
    {
        public bool IsComposite() => false;
        public Coordinate Coordinate { get; set; }

        public void Accept(ISudokuComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IEnumerable<ISudokuComponent> GetChildren()
        {
            throw new System.NotImplementedException();
        }
    }
}