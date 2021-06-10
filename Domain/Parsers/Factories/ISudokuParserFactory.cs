namespace Sudoku.Domain.Parsers.Factories
{
    public interface ISudokuParserFactory
    {
        ISudokuParser CreateParser();
    }
}