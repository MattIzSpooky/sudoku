using Sudoku.Domain.Board;
using Sudoku.Domain.Board.Leaves;

namespace Sudoku.Domain.Visitors
{
    public interface ISudokuComponentVisitor
    {
        public void Visit(WallLeaf wallLeaf);
        public void Visit(CellLeaf cell);
        public void Visit(QuadrantComposite quadrantComposite);
    }
}