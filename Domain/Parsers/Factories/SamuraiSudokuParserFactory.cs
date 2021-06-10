namespace Sudoku.Domain.Parsers.Factories
{
    public class SamuraiSudokuParserFactory : ISudokuParserFactory
    {
        public ISudokuParser CreateParser()
        {
            return new SamuraiSudokuParser();
        }
    }
}