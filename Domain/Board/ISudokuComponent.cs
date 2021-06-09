using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Domain.Board.Visitors;

namespace Sudoku.Domain.Board
{
    public interface ISudokuComponent
    {
        bool IsComposite();
        bool IsValid();
        void Accept(IVisitor visitor)
        {
            visitor.VisitCell(this);
        }
        
        public IEnumerable<ISudokuComponent> GetChildren();
    }
}