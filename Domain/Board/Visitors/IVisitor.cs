﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Board.Visitors
{
    public interface IVisitor
    {
        void VisitCell(ISudokuComponent cell);
    }
}