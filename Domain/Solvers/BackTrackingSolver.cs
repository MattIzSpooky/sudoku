using System.Linq;
using Domain.Board;

namespace Domain.Solvers
{
    public class BackTrackingSolver : ISolverStrategy
    {
        public bool Solve(Field field)
        {
            var emptyCell = field.GetOrderedCells().FirstOrDefault(c => c.Value.DefinitiveValue == 0 && !c.IsLocked);
            var maxValue = field.GetMaxValue();

            if (emptyCell == null) return true;

            for (var i = 1; i <= maxValue; ++i)
            {
                emptyCell.Value.DefinitiveValue = i;

                field.Validate();
                
                if (field.IsValid() && Solve(field)) return true;

                emptyCell.Value.DefinitiveValue = 0;
            }

            return false;
        }
    }
}