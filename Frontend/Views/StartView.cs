using System.Drawing;
using MVC.Views.Console;

namespace Frontend.Views
{
    public class StartView : ConsoleView
    {
        public StartView() : base(30, 30, "Sudoku")
        {
        }

        public override void Draw()
        {
            ClearBuffer();
            
            WriteWelcome();
            WriteInstructions();
            
            WriteBuffer();
        }

        private void WriteWelcome()
        {
            WriteString("Welcome to the Temple of Doom!", Color.OrangeRed);
            StringCursor++;
        }

        private void WriteInstructions()
        {
            WriteString("Press Space to continue..", Color.MediumVioletRed);
            WriteString("Press Esc to exit..", Color.Goldenrod);
        }
    }
}