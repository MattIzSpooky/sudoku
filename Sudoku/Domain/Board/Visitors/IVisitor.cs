using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Board.Visitors
{
    public class IVisitor
    {
        public void VisitCell(ICell cell)
        {
            throw new System.NotImplementedException();
        }
    }
}