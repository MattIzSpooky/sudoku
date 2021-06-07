using Sudoku.Domain.Board.GridItems;

namespace Sudoku.Domain.Board.Builders
{
    public class GridBuilder : IBuilder<Grid>
    {
        private Grid _grid = new();

        private int _x;
        private int _y;
        
        private readonly int _offsetX;
        private readonly int _offsetY;

        public GridBuilder(int offsetX, int offsetY)
        {
            _x = offsetX;
            _y = offsetY;

            _offsetX = offsetX;
            _offsetY = offsetY;

            _grid.SetOffsetX(offsetX);
            _grid.SetOffsetY(offsetY);
        }
        
        public void BuildWall(bool isHorizontal = false)
        {
            _grid.Add(new Wall(isHorizontal)
            {
                X = _x,
                Y = _y
            });

            _x++;
        }

        public void BuildCell(CellLeaf cellLeaf)
        {
            _grid.Add(new Cell(cellLeaf)
            {
                X = _x,
                Y = _y
            });
            
            _x++;
        }
        
        public void BuildEmptySpace()
        {
            _grid.Add(new EmptySpace()
            {
                X = _x,
                Y = _y
            });
            
            _x++;
        }

        public void InsertRow()
        {
            _x = _offsetX;
            _y++;
        }
        
        public Grid GetResult()
        {
            return _grid;
        }

        public void Reset()
        {
            _grid = new Grid();
            
            _x -= _offsetX;
            _y -= _offsetY;
        }
    }
}