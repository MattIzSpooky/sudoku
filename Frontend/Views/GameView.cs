using System.Collections.Generic;
using System.Drawing;
using Sudoku.Domain.Board;
using Sudoku.Frontend.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Frontend.Views
{
    public class GameView : ConsoleView
    {
        public IReadOnlyList<Field>? Grids { private get; set; }
        public Coordinate Cursor { private get; set; }
        public string? StateName { private get; set; }

        public GameView(string gameName) : base(55, 45, $"Sudoku: {gameName}")
        {
        }

        protected override void FillBuffer()
        {
            if(Grids == null) return;
            
            foreach (var grid in Grids)
            {
                foreach (var quadrant in grid.Quadrants)
                {
                    quadrant.Accept(new RenderVisitor(Buffer));
                }
            }

            Buffer[Cursor.Y][Cursor.X] = CreateChar('X', Color.Lime);

            StringCursor = Height - 15;

            WriteString($"State: {StateName}", Color.Chartreuse);
            WriteString("Entering the same digit removes the value", Color.Purple);

            StringCursor++;
            
            WriteString("[Space bar]: Switch between modes", Color.White);
            WriteString("[S]: Let computer solve the Sudoku", Color.White);
            WriteString("[C]: Validate the puzzle", Color.White);
            WriteString("[1-9]: Place digit", Color.White);
            WriteString("[0] & [DELETE]: = Remove digit", Color.White);
            WriteString("[UP, LEFT, BOTTOM, DOWN Arrows]: Moving the cursor", Color.White);
            WriteString("[ESC]: Go back to start", Color.White);
        }
    }
}