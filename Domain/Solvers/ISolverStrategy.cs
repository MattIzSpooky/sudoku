using Domain.Board;

namespace Domain.Solvers
{
    public interface ISolverStrategy
    {
        bool Solve(Field field);
    }
}