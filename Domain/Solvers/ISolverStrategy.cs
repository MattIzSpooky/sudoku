using Sudoku.Domain.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Solvers
{
    public interface ISolverStrategy
    {
        void Solve(Board.Sudoku sudoku);
    }
}