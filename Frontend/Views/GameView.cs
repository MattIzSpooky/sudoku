using System.Collections.Generic;
using Sudoku.Domain.Board.GridItems;
using Sudoku.Frontend.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Frontend.Views
{
    public class GameView : ConsoleView
    {
        public IEnumerable<IGridItem> GridItems { private get; set; }

        public GameView() : base(30, 30, "Sudoku")
        {
        }

        protected override void FillBuffer()
        {
            var visitor = new RenderVisitor();

            foreach (var part in GridItems)
            {
                part.Accept(Buffer, visitor);
            }
        }
    }
}