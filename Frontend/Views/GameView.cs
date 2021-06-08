using System.Drawing;
using Sudoku.Domain.Board;
using Sudoku.Frontend.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Frontend.Views
{
    public class GameView : ConsoleView
    {
        public Grid[] Grids { private get; set; }
        public Coordinate Cursor { private get; set; }

        public GameView() : base(45, 45, "Sudoku")
        {
        }

        protected override void FillBuffer()
        {
            foreach (var grid in Grids)
            {
                grid.Accept(new RenderVisitor(Buffer) {Cursor = Cursor});
            }

            Buffer[Cursor.Y][Cursor.X] = CreateChar('X', Color.Lime);

            StringCursor = Height - 6;

            WriteString("Controls", Color.White);
            WriteString("Space bar -> Switch between modes", Color.White);
            WriteString("S -> Let computer solve the Sudoku", Color.White);
            WriteString("C -> Validate the puzzle", Color.White);

            WriteString($"Cursor PositionX: {Cursor.X}", Color.Gold);
            WriteString($"Cursor PositionY: {Cursor.Y}", Color.Gold);
        }
    }
}