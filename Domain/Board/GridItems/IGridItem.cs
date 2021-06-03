using Sudoku.Domain.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Domain.Board.GridItems
{
    public interface IGridItem
    {
        public void Accept(ColoredChar[][] param, IGridItemVisitor<ColoredChar[][]> visitor);
    }
}