using System.Collections.Generic;
using System.Drawing;
using Domain.Selector;
using MVC.Views.Console;

namespace Frontend.Views
{
    public class StartView : ConsoleView
    {
        public IReadOnlyList<SudokuFile> SudokuFiles { private get; set; } = new List<SudokuFile>();
        public string Error { private get; set; } = string.Empty;

        public StartView() : base(70, 40, "Sudoku")
        {
        }

        protected override void FillBuffer()
        {
            if (Error != string.Empty)
            {
                WriteString(Error, Color.Crimson);
                return;
            }

            foreach (var sudokuFile in SudokuFiles)
            {
                var selectedChar = sudokuFile.IsSelected ? '*' : ' ';

                WriteString($"[{selectedChar}]: {sudokuFile.Name}", Color.Lime);
            }

            WriteInstructions();
        }

        private void WriteInstructions()
        {
            StringCursor++;
            WriteString("Use UP and DOWN to select a Sudoku", Color.Goldenrod);
            WriteString("Press Space to start..", Color.Goldenrod);
            WriteString("Press Esc to exit..", Color.Goldenrod);
        }
    }
}