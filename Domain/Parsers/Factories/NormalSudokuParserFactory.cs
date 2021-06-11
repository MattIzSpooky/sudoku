namespace Domain.Parsers.Factories
{
    public class NormalSudokuParserFactory : ISudokuParserFactory
    {
        public ISudokuParser CreateParser()
        {
            return new NormalSudokuParser();
        }
    }
}