using Sudoku.Domain.Board.GridItems;

namespace Sudoku.Domain.Visitors
{
    public interface IGridItemVisitor
    {
        public void Visit(Wall wall);
        public void Visit(Cell cell);
        public void Visit(EmptySpace emptySpace);
    }
}