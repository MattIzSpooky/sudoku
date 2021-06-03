using System.Text;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board.GridItems
{
    public class EmptySpace : IGridItem
    {
        public void Accept(StringBuilder param, IGridItemVisitor<StringBuilder> visitor)
        {
            visitor.Visit(param, this);
        }
    }
}