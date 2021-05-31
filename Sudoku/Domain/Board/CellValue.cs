using System.Runtime.InteropServices.ComTypes;

namespace Sudoku.Domain.Board
{
    public class CellValue
    {
        public int Value {get; set; }
        public int HelpNumber { get; set; }

        public CellValue(int value)
        {
            Value = value;
        }
    }   
}