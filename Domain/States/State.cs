using Sudoku.Domain.Board;

namespace Sudoku.Domain.States
{
    public abstract class State
    {
        protected Game? Game { get; private set; }
        public void SetGame(Game game) => Game = game;
        public abstract void Handle(CellLeaf cellLeaf, int value);
        public abstract void ChangeState();
        public abstract Grid[]? CreateGrid();
    }
}