using System;
using System.Linq;
using Sudoku.Domain.Board;
using Sudoku.Domain.Board.GridItems;
using Sudoku.Domain.Solvers;
using Sudoku.Domain.States;

namespace Sudoku.Domain
{
    public class Game
    {
        private readonly Context _context;
        public Grid[] Grids { get; }

        private Coordinate _cursor;
        public Coordinate Cursor => _cursor;

        private readonly Coordinate _maxCords;

        public Game(Board.Sudoku[]? grids)
        {
            _context = new Context();

            _context.SetSudoku(grids);
            _context.TransitionTo(new DefinitiveState());
            _context.SetStrategy(new BackTrackingSolver());
            
            Grids = _context.Construct();
            
            _maxCords = GetMaxDimensions();
        }

        public void MoveCursor(int x, int y)
        {
            var newCords = new Coordinate(_cursor.X + x, _cursor.Y + y);
            
            if (newCords.X < 0 || newCords.Y < 0 || newCords.X > _maxCords.X || newCords.Y > _maxCords.Y) return;

            _cursor = newCords;
        }

        private Coordinate GetMaxDimensions()
        {
            var last = Grids.Last();
            var count = (int) Math.Sqrt(last.GridItems.Count);
            return new Coordinate(last.OffsetX + count - 1, last.OffsetY + count - 1);
        }
    }
}