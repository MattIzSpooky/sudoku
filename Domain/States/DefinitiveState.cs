using Sudoku.Domain.Board;

namespace Sudoku.Domain.States
{
    public class DefinitiveState : State
    {
        public override void Handle(CellLeaf cellLeaf, int value)
        {
            cellLeaf.Value.Value = cellLeaf.Value.Value == value ? 0 : value;
        }

        public override void ChangeState()
        {
            Game?.TransitionTo(new AuxiliaryState());
        }
    }
}