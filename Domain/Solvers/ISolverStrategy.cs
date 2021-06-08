using Sudoku.Domain.Board;

namespace Sudoku.Domain.Solvers
{
    public interface ISolverStrategy
    {
        bool Solve(Field field);
    }
}