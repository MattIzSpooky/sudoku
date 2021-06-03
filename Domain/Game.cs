using Sudoku.Domain.Board;
using Sudoku.Domain.Solvers;
using Sudoku.Domain.States;

namespace Sudoku.Domain
{
    public class Game
    {
        private readonly Context _context;
        public Grid[] Grids { get; }

        public Game(Board.Sudoku[]? grids)
        {
            _context = new Context();

            _context.SetSudoku(grids);
            _context.TransitionTo(new DefinitiveState());
            _context.SetStrategy(new BackTrackingSolver());

            Grids = _context.Construct();
        }
    }
}