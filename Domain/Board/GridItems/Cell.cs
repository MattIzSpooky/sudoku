using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board.GridItems
{
    public class Cell : IGridItem
    {
        public CellLeaf CellLeaf { get; }
        public Cell(CellLeaf cellLeaf)
        {
            CellLeaf = cellLeaf;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public void Accept(IGridItemVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}