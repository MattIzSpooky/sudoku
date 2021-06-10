using Sudoku.Domain.Board.Leaves;

namespace Sudoku.Domain.Visitors
{
    public interface ISudokuComponentVisitor
    {
        public void Visit(WallLeaf wallLeaf);
        public void Visit(CellLeaf cell);
    }
}