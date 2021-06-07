using System;
using Sudoku.Domain.Board;

namespace Sudoku.Domain.States
{
    public class AuxiliaryState : State
    {
        public override void Handle(CellLeaf cellLeaf)
        {
            
        }

        public override void ChangeState()
        {
            Context?.TransitionTo(new DefinitiveState());
        }

        public override Grid[]? CreateGrid()
        {
            throw new NotImplementedException();
        }
    }
}