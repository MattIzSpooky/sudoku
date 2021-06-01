using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Board.Visitors
{
    public class ToggleVisitor : IVisitor
    {
        public void VisitCell(ISudokuComponent cell)
        {
            throw new NotImplementedException();
        }
    }
}