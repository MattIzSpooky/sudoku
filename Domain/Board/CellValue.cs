namespace Sudoku.Domain.Board
{
    public class CellValue
    {
        public int DefinitiveValue {get; set; }
        public int HelpNumber { get; set; }

        public CellValue(int definitiveValue)
        {
            DefinitiveValue = definitiveValue;
        }
    }   
}