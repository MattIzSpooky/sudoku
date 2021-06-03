using Sudoku.Domain.Board;
using Sudoku.Domain.Solvers;
using Sudoku.Domain.States;

namespace Sudoku.Domain
{
    public class Game
    {
        private readonly Context _context;
        public Grid Grid { get; }

        public Game(Board.Sudoku grid)
        {
            _context = new Context();

            _context.SetSudoku(grid);
            _context.TransitionTo(new DefinitiveState());
            _context.SetStrategy(new BackTrackingSolver());

            Grid = _context.Construct();
        }
    }
}