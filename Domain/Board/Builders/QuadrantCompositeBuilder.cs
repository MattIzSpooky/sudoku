

using Sudoku.Domain.Board.Leaves;

namespace Sudoku.Domain.Board.Builders
{
    public class QuadrantCompositeBuilder : IBuilder<QuadrantComposite>
    {
        private QuadrantComposite _quadrant = new();

        private int _x;
        private int _y;
        
        private readonly int _offsetX;
        private readonly int _offsetY;

        public QuadrantCompositeBuilder(int offsetX, int offsetY)
        {
            _x = offsetX;
            _y = offsetY;

            _offsetX = offsetX;
            _offsetY = offsetY;
        }
        
        public void BuildWall(bool isHorizontal = false)
        {
            _quadrant.AddComponent(new Wall(isHorizontal)
            {
                Coordinate = new Coordinate(_x, _y)
            });

            _x++;
        }

        public void BuildCell(CellLeaf cellLeaf)
        {
            _quadrant.AddComponent(cellLeaf);
            
            _x++;
        }
        
        public void BuildEmptySpace()
        {
            _quadrant.AddComponent(new EmptySpace()
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
        
        public QuadrantComposite GetResult()
        {
            return _quadrant;
        }

        public void Reset()
        {
            _quadrant = new QuadrantComposite();
            
            _x -= _offsetX;
            _y -= _offsetY;
        }
    }
}