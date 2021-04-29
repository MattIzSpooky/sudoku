using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Domain.Board.Visitors;

namespace Sudoku.Domain.Board
{
    public interface ICell
    {
        bool IsEditable { get; protected set; }

        void ToggleEditable()
        {
            this.IsEditable = !this.IsEditable;
        }
        bool IsComposite();
        bool IsValid();
        void Accept(IVisitor visitor)
        {
            visitor.VisitCell(this);
        }
    }
}