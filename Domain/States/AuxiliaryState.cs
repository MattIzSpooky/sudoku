using System;
using Sudoku.Domain.Board;

namespace Sudoku.Domain.States
{
    public class AuxiliaryState : State
    {
        public override void Handle()
        {
            throw new NotImplementedException();
        }

        public override Grid[]? CreateGrid()
        {
            throw new NotImplementedException();
        }
    }
}