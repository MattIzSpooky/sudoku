using System;
using System.Drawing;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Frontend.Views
{
    public class GameView: ConsoleView
    {
        public GameView() : base(30, 30, "Sudoku")
        {
        }

        protected override void FillBuffer()
        {
            WriteString($"Welcome", Color.Fuchsia);
        }
    }
}