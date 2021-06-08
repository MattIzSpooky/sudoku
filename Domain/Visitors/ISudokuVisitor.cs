using Sudoku.Domain.Board;

namespace Sudoku.Domain.Visitors
{
    public interface ISudokuVisitor
    {
        public Grid Visit(Board.Field field);
    }
}