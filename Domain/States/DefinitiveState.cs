using Sudoku.Domain.Board.Leaves;

namespace Sudoku.Domain.States
{
    public class DefinitiveState : State
    {
        public override void Handle(CellLeaf cellLeaf, int value)
        {
            cellLeaf.Value.DefinitiveValue = cellLeaf.Value.DefinitiveValue == value ? 0 : value;
        }

        public override void ChangeState()
        {
            Context?.TransitionTo(new AuxiliaryState());
        }
    }
}