using System.Collections.Generic;
using System.Drawing;
using Sudoku.Domain.Selector;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Frontend.Views
{
    public class StartView : ConsoleView
    {
        public IReadOnlyList<SudokuFile> SudokuFiles { private get; set; } = new List<SudokuFile>();
        public StartView() : base(70, 40, "Sudoku")
        {
        }

        protected override void FillBuffer()
        {
            foreach (var sudokuFile in SudokuFiles)
            {
                var selectedChar = sudokuFile.IsSelected ? 'X' : ' ';
                
                WriteString($"[{selectedChar}]: {sudokuFile.Name}", Color.Lime);
            }

            WriteInstructions();
        }

        private void WriteInstructions()
        {
            StringCursor++;
            WriteString("Press Space to start..", Color.Goldenrod);
            WriteString("Press Esc to exit..", Color.Goldenrod);
        }
    }
}