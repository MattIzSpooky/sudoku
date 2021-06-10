namespace Sudoku.Domain.Parsers.Factories
{
    public class JigsawSudokuParserFactory : ISudokuParserFactory
    {
        public ISudokuParser CreateParser()
        {
            return new JigsawSudokuParser();
        }
    }
}