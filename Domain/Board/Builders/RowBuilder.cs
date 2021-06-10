using Sudoku.Domain.Board.Leaves;
using Sudoku.Domain.Parsers;

namespace Sudoku.Domain.Board.Builders
{
    public class RowBuilder : IBuilder<Row>
    {
        private Row _row = new();

        private int _x;
        private int _y;

        private readonly int _offsetX;
        private readonly int _offsetY;

        public RowBuilder(int offsetX, int offsetY)
        {
            _x = offsetX;
            _y = offsetY;

            _offsetX = offsetX;
            _offsetY = offsetY;
        }

        public void BuildWall(bool isHorizontal = false)
        {
            _row.Add(new Wall(isHorizontal)
            {
                Coordinate = new Coordinate(_x, _y)
            });

            _x++;
        }

        public void BuildCell(int value)
        {
            var cell = new CellLeaf(new Coordinate(_x, _y), value) {IsLocked = value != 0};
            _row.Add(cell);
            _x++;
        }

        public void BuildEmptySpace()
        {
            _row.Add(new EmptySpace()
            {
                Coordinate = new Coordinate(_x, _y)
            });

            _x++;
        }

        public void SetY(int y)
        {
            _y = _offsetY + y;
        }

        public Row GetResult()
        {
            return _row;
        }

        public void Reset()
        {
            _row = new Row();

            _x = _offsetX;
            _y = _offsetY;
        }
    }
}