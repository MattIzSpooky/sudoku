using System.Text;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board.GridItems
{
    public interface IGridItem
    {
        public void Accept(StringBuilder param, IGridItemVisitor<StringBuilder> visitor);
    }
}