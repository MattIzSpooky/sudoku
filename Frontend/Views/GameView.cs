using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board;
using Sudoku.Frontend.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Frontend.Views
{
    public class GameView : ConsoleView
    {
        public Grid[] Grids { private get; set; }


        public GameView() : base(30, 30, "Sudoku")
        {
        }

        protected override void FillBuffer()
        {
            foreach (var grid in Grids)
            {
                var visitor = new RenderVisitor {OffsetX = grid.OffsetX, OffsetY = grid.OffsetY};

                foreach (var item in grid.GridItems)
                {
                    item.Accept(Buffer, visitor);
                }
            }
        }
    }
}