using System.Drawing;
using Sudoku.Domain.Board;
using Sudoku.Domain.Board.GridItems;
using Sudoku.Domain.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Frontend.Visitors
{
    public class RenderVisitor : IGridItemVisitor<ColoredChar[][]>
    {
        public Coordinate Cursor { get; set; }

        public void Visit(ColoredChar[][] param, Wall wall)
        {
            param[wall.Y][wall.X] = new ColoredChar()
            {
                Character = wall.Horizontal ? '-' : '|'
            };
        }

        public void Visit(ColoredChar[][] param, Cell cell)
        {
            var cellLeafValue = cell.CellLeaf.Value;
            var definitiveValue = cellLeafValue.Value;
            var character = ' ';
            var color = Color.White;

            if (definitiveValue == 0 && cellLeafValue.HelpNumber > 0)
            {
                character = char.Parse(cellLeafValue.HelpNumber.ToString());
                color = Color.Gold;
            }
            else
            {
                character = definitiveValue == 0 ? ' ' : char.Parse(definitiveValue.ToString());
            }

            param[cell.Y][cell.X] = new ColoredChar()
            {
                Character = character,
                Color = color
            };
        }

        public void Visit(ColoredChar[][] param, EmptySpace emptySpace)
        {
            param[emptySpace.Y][emptySpace.X] = new ColoredChar()
            {
                Character = ' '
            };
        }
    }
}