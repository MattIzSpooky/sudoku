namespace Domain.Parsers.Factories
{
    public interface ISudokuParserFactory
    {
        ISudokuParser CreateParser();
    }
}