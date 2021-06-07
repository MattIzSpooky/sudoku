using Sudoku.Domain.Board;

namespace Sudoku.Domain.States
{
    public abstract class State
    {
        protected Context? Context;
        
        public abstract void Handle(CellLeaf cellLeaf, int value);
        public abstract void ChangeState();
        public abstract Grid[]? CreateGrid();

        public void SetContext(Context context)
        {
            Context = context;
        }
    }
}