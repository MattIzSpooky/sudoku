using Sudoku.Domain.Board.GridItems;

namespace Sudoku.Domain.Board.Builders
{
    public class GridBuilder : IBuilder<Grid>
    {
        private Grid _grid = new();
        
        public void BuildWall(bool isHorizontal = false)
        {
            _grid.Add(new Wall(isHorizontal));
        }

        public void BuildCell(CellLeaf cellLeaf)
        {
            _grid.Add(new Cell(cellLeaf));
        }
        
        public void BuildEmptySpace()
        {
            _grid.Add(new EmptySpace());
        }

        public void BuildRow()
        {
            _grid.Add(new Row());
        }
        
        public Grid GetResult()
        {
            return _grid;
        }

        public void Reset()
        {
            _grid = new Grid();
        }
    }
}