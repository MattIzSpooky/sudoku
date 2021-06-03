using System;
using System.Linq;
using Sudoku.Domain.Board;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.States
{
    public class DefinitiveState : State
    {
        public override void Handle()
        {
            throw new NotImplementedException();
        }

        public override Grid[]? CreateGrid()
        {
            return Context?.Sudoku?.Select(sdk => sdk.Accept(new SudokuVisitor())).ToArray();
        }
    }
}