using Sudoku.Domain.Board;
namespace Sudoku.Domain.Parsers
{
    public interface ISudokuParser
    {
        Grid Parse(string fileContent);
    }
}