using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board;
using Sudoku.Domain.Board.Leaves;
using Sudoku.Domain.States;

namespace Sudoku.Domain
{
    public class Game
    {
        private readonly Field[] _fields;
        public IReadOnlyList<Field> Fields => _fields;
        private State _state;

        private Coordinate _cursor;
        public Coordinate Cursor => _cursor;

        private readonly Coordinate _maxCords;
        private readonly int _maxValue;

        public Game(Field[] fields)
        {
            _fields = fields;
            TransitionTo(new DefinitiveState());

            _maxValue = _fields[0].GetMaxValue();
            _maxCords = GetMaxCoordinates();
        }

        private Coordinate GetMaxCoordinates() => _fields.Last().GetMaxCoordinates();

        public void EnterValue(int value)
        {
            if (value > _maxValue) return;

            var selectedCell = _fields
                .SelectMany(g => g.Quadrants)
                .Select(quadrant => quadrant.CellByCoordinate(_cursor))
                .FirstOrDefault(c => c != null);

            if (selectedCell is {IsLocked: false})
                _state.Handle(selectedCell, value);
        }

        public void MoveCursor(int x, int y)
        {
            var newCords = new Coordinate(_cursor.X + x, _cursor.Y + y);

            if (newCords.X < 0 || newCords.Y < 0 || newCords.X > _maxCords.X || newCords.Y > _maxCords.Y) return;

            _cursor = newCords;
        }

        public string GetStateName() => _state.GetName();

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