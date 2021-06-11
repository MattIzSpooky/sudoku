using System.Collections.Generic;
using Domain.Visitors;

namespace Domain.Board
{
    public interface ISudokuComponent
    {
        bool IsComposite();
        public Coordinate Coordinate { get; }
        void Accept(ISudokuComponentVisitor visitor);
        public IEnumerable<ISudokuComponent> GetChildren();
    }
}