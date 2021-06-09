using System;
using System.Linq;
using Sudoku.Domain.Board;
using Sudoku.Domain.Board.GridItems;
using Sudoku.Domain.States;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain
{
    public class Game
    {
        public Grid[] Grids { get; }
        private readonly Field[] _fields;
        private State _state;
        
        private Coordinate _cursor;
        public Coordinate Cursor => _cursor;

        private readonly Coordinate _maxCords;
        private readonly int _maxValue;

        public Game(Field[] fields)
        {
            _fields = fields;
            TransitionTo(new DefinitiveState());

            Grids = CreateGrid();
            
            _maxCords = GetMaxCoordinates();
            _maxValue = _fields[0].MaxValue;
        }

        private Grid[] CreateGrid()
        {
            return _fields.Select(sdk => sdk.Accept(new SudokuVisitor())).ToArray();
        }
        
        public void EnterValue(int value)
        {
            if (value > _maxValue) return;

            var selectedCell = Grids
                .SelectMany(g => g.GridItems.OfType<Cell>())
                .FirstOrDefault(g => g.X == _cursor.X && g.Y == _cursor.Y);
            
            if (selectedCell != null && !selectedCell.CellLeaf.IsLocked) 
                _state.Handle(selectedCell.CellLeaf, value);
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
            return new Coordinate(last.OffsetX + count, last.OffsetY + count);
        }

        public void SwitchState() => _state.ChangeState();

        public void Validate()
        {
            foreach (var field in _fields)
            {
                field.Validate();
            }
        }
        
        public void TransitionTo(State newState)
        {
            _state = newState;
            _state.SetContext(this);
        }

        public void Solve()
        {
            foreach (var field in _fields)
            {
                field.Solve();
            }
        }
    }
}