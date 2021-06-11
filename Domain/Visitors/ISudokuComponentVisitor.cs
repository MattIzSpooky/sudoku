using Domain.Board;
using Domain.Board.Leaves;

namespace Domain.Visitors
{
    public interface ISudokuComponentVisitor
    {
        public void Visit(WallLeaf wallLeaf);
        public void Visit(CellLeaf cell);
        public void Visit(QuadrantComposite quadrantComposite);
    }
}