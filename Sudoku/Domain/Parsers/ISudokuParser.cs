using Sudoku.Domain.Board;

namespace Sudoku.Domain.Parsers
{
    public interface ISudokuParser
    {
        public Grid Parse(string content);
    }
}