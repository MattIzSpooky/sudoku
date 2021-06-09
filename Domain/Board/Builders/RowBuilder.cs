

using Sudoku.Domain.Board.Leaves;
using Sudoku.Domain.Parsers;

namespace Sudoku.Domain.Board.Builders
{
    public class RowBuilder : IBuilder<Row>
    {
        private Row _items = new();

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
            _items.Add(new Wall(isHorizontal)
            {
                Coordinate = new Coordinate(_x, _y)
            });

            _x++;
        }

        public void BuildCell(CellLeaf cellLeaf)
        {
            var clone = cellLeaf.Clone();
            clone.Coordinate = new Coordinate(_x, _y);
            _items.Add(clone);
            
            _x++;
        }
        
        public void BuildEmptySpace()
        {
            _items.Add(new EmptySpace()
            {
                Coordinate = new Coordinate(_x, _y)
            });
            
            _x++;
        }

        public void InsertRow()
        {
            _x = _offsetX;
            _y++;
        }
        
        public Row GetResult()
        {
            return _items;
        }

        public void Reset()
        {
            _items = new Row();
            
            _x = _offsetX;
            _y = _offsetY;
        }
    }
}