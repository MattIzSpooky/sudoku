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
        private readonly ISolverStrategy _strategy;
        public Grid[] Grids { get; }
        public Field[] Fields { get; }
        private State? _state;
        
        private Coordinate _cursor;
        public Coordinate Cursor => _cursor;

        private readonly Coordinate _maxCords;
        private readonly int _maxValue;

        public Game(Field[] fields)
        {
            Fields = fields;
            
            _strategy = new BackTrackingSolver();
            TransitionTo(new DefinitiveState());
            
            Grids = _state?.CreateGrid() ?? Array.Empty<Grid>();
            
            _maxCords = GetMaxCoordinates();
            _maxValue = Fields[0].MaxValue;
        }
        
        public void EnterValue(int value)
        {
            if (value > _maxValue) return;

            var selectedCell = Grids
                .SelectMany(g => g.GridItems.OfType<Cell>())
                .FirstOrDefault(g => g.X == _cursor.X && g.Y == _cursor.Y);
            
            if (selectedCell != null && !selectedCell.CellLeaf.IsLocked) 
                _state?.Handle(selectedCell.CellLeaf, value);
        }

        public void MoveCursor(int x, int y)
        {
            var newCords = new Coordinate(_cursor.X + x, _cursor.Y + y);
            
            if (newCords.X < 0 || newCords.Y < 0 || newCords.X > _maxCords.X || newCords.Y > _maxCords.Y) return;

            _cursor = newCords;
        }

        private Coordinate GetMaxCoordinates()
        {
            var last = Grids.Last();
            var count = (int) Math.Sqrt(last.GridItems.Count);
            return new Coordinate(last.OffsetX + count - 1, last.OffsetY + count - 1);
        }

        public void SwitchState()
        {
            _state?.ChangeState();
        }

        public void ValidateNumbers()
        {
            foreach (var field in Fields)
            {
                field.Validate();
            }
        }
        
        public void TransitionTo(State newState)
        {
            _state = newState;
            _state.SetGame(this);
        }

        public void Solve()
        {
            foreach (var field in Fields)
            {
                _strategy.Solve(field);
            }
        }
    }
}