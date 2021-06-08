using Sudoku.Domain.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Solvers
{
    public interface ISolverStrategy
    {
        bool Solve(Board.Field field);
    }
}