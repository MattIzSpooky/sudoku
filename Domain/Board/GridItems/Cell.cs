using System.Text;
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

        public void Accept(StringBuilder param, IGridItemVisitor<StringBuilder> visitor)
        {
            visitor.Visit(param, this);
        }
    }
}