using Sudoku.Domain.Board;

namespace Sudoku.Domain.Parsers
{
    public interface ISudokuParser
    {
        public Field[] Parse(string content, int offsetX = 0, int offsetY = 0);
    }
}