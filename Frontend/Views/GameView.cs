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

        public GameView(string gameName) : base(45, 45, $"Sudoku: {gameName}")
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
            
            StringCursor++;
            
            WriteString("Controls", Color.White);
            WriteString("Space bar = Switch between modes", Color.White);
            WriteString("S = Let computer solve the Sudoku", Color.White);
            WriteString("C = Validate the puzzle", Color.White);
        }
    }
}