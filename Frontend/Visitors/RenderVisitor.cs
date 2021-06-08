using System.Drawing;
using Sudoku.Domain.Board;
using Sudoku.Domain.Board.GridItems;
using Sudoku.Domain.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Frontend.Visitors
{
    public class RenderVisitor : IGridItemVisitor
    {
        public Coordinate Cursor { get; set; }

        private readonly ColoredChar[][] _buffer;

        public RenderVisitor(ColoredChar[][] buffer)
        {
            _buffer = buffer;
        }
        
        public void Visit(Wall wall)
        {
            _buffer[wall.Y][wall.X] = new ColoredChar()
            {
                Character = wall.Horizontal ? '-' : '|'
            };
        }

        public void Visit(Cell cell)
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

            _buffer[cell.Y][cell.X] = new ColoredChar()
            {
                Character = character,
                Color = color
            };
        }

        public void Visit(EmptySpace emptySpace)
        {
            _buffer[emptySpace.Y][emptySpace.X] = new ColoredChar()
            {
                Character = ' '
            };
        }
    }
}