using Sudoku.Domain.Board;

namespace Sudoku.Domain.States
{
    public abstract class State
    {
        protected Game? Context { get; private set; }
        public void SetContext(Game context) => Context = context;
        public abstract void Handle(CellLeaf cellLeaf, int value);
        public abstract void ChangeState();
    }
}