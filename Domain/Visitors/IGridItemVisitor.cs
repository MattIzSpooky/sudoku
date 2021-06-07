using Sudoku.Domain.Board.GridItems;

namespace Sudoku.Domain.Visitors
{
    public interface IGridItemVisitor<in T>
    {
        public void Visit(T param, Wall wall);
        public void Visit(T param, Cell cell);
        public void Visit(T param, Row row);
        public void Visit(T param, EmptySpace emptySpace);
    }
}