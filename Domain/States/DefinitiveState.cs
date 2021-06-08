using System;
using System.Linq;
using Sudoku.Domain.Board;
using Sudoku.Domain.Visitors;

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

        public override Grid[]? CreateGrid()
        {
            return Game?.Fields.Select(sdk => sdk.Accept(new SudokuVisitor())).ToArray();
        }
    }
}