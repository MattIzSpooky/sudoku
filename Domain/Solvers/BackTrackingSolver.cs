using Sudoku.Domain.Board;
using System.Linq;

namespace Sudoku.Domain.Solvers
{
    public class BackTrackingSolver : ISolverStrategy
    {
        public bool Solve(Field field)
        {
            var emptyCell = field.GetOrderedCells().FirstOrDefault(c => c.Value.Value == 0 && !c.IsLocked);
            var maxValue = field.MaxValue;

            if (emptyCell == null) return true;

            for (var i = 1; i <= maxValue; ++i)
            {
                emptyCell.Value.Value = i;

                if (field.Validate() && Solve(field)) return true;

                emptyCell.Value.Value = 0;
            }

            return false;
        }
    }
}