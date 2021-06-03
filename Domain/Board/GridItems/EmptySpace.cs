using Sudoku.Domain.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Domain.Board.GridItems
{
    public class EmptySpace : IGridItem
    {
        public void Accept(ColoredChar[][] param, IGridItemVisitor<ColoredChar[][]> visitor)
        {
            visitor.Visit(param, this);
        }
    }
}