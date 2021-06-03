using System;
using System.Collections.Generic;
using System.Text;
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
            var builder = new StringBuilder();
            var visitor = new RenderVisitor();

            foreach (var part in GridItems)
            {
                part.Accept(builder, visitor);
            }

            var rows = builder.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var y = 0;
            foreach (var row in rows)
            {
                for (var x = 0; x < row.Length; x++)
                {
                    Buffer[y][x] = CreateChar(row[x]);
                }

                y++;
            }
        }
    }
}