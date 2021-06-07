using Sudoku.Domain.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Domain.Board.GridItems
{
    public interface IGridItem
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Accept(ColoredChar[][] param, IGridItemVisitor<ColoredChar[][]> visitor);
    }
}