using Sudoku.Domain.Board;

namespace Sudoku.Domain
{
    public class Game
    {
        private Grid[] _grids;
        public Game(Grid[] grids)
        {
            _grids = grids;
        }
    }
}
