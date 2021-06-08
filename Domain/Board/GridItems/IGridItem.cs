using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board.GridItems
{
    public interface IGridItem
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Accept(IGridItemVisitor visitor);
    }
}