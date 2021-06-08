using Sudoku.Domain.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Domain.Board.GridItems
{
    public class EmptySpace : IGridItem
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Accept(IGridItemVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}