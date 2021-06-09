using Sudoku.Domain.Board.Leaves;

namespace Sudoku.Domain.Visitors
{
    public interface ISudokuComponentVisitor
    {
        public void Visit(Wall wall);
        public void Visit(CellLeaf cell);
        public void Visit(EmptySpace emptySpace);
    }
}