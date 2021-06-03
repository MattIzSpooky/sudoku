using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
                var visitor = new RenderVisitor {OffsetX = grid.OffsetX, OffsetY = grid.OffsetY, Cursor = Cursor};

                foreach (var item in grid.GridItems)
                {
                    item.Accept(Buffer, visitor);
                }
            }
            Buffer[Cursor.Y][Cursor.X] = CreateChar('X', Color.Lime);

            StringCursor = Height - 2;
            
            WriteString($"Cursor PositionX: {Cursor.X}", Color.Gold);
            WriteString($"Cursor PositionY: {Cursor.Y}", Color.Gold);
        }
    }
}