using System.Collections.Generic;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board
{
    public interface ISudokuComponent
    {
        bool IsComposite();
        public Coordinate Coordinate { get; }
        void Accept(ISudokuComponentVisitor visitor);
        public IEnumerable<ISudokuComponent> GetChildren();
    }
}