﻿using Sudoku.Domain.Board;

namespace Sudoku.Domain.States
{
    public class AuxiliaryState : State
    {
        public override void Handle(CellLeaf cellLeaf, int value)
        {
            if (cellLeaf.Value.DefinitiveValue > 0)
                return;
            
            cellLeaf.Value.HelpNumber = cellLeaf.Value.HelpNumber == value ? 0 : value;
        }

        public override void ChangeState()
        {
            Context?.TransitionTo(new DefinitiveState());
        }
    }
}