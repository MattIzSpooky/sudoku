using System.Drawing;
using Sudoku.Domain.Board.Leaves;
using Sudoku.Domain.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Frontend.Visitors
{
    public class RenderVisitor : ISudokuComponentVisitor
    {

        private readonly ColoredChar[][] _buffer;

        public RenderVisitor(ColoredChar[][] buffer)
        {
            _buffer = buffer;
        }
        
        public void Visit(Wall wall)
        {
            _buffer[wall.Coordinate.Y][wall.Coordinate.X] = new ColoredChar()
            {
                Character = wall.Horizontal ? '-' : '|'
            };
        }

        public void Visit(CellLeaf cell)
        {
            var cellLeafValue = cell.Value;
            var definitiveValue = cellLeafValue.DefinitiveValue;
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
            
            if(!cell.IsValid) color = Color.Red;

            _buffer[cell.Coordinate.Y][cell.Coordinate.X] = new ColoredChar()
            {
                Character = character,
                Color = color
            };
        }

        public void Visit(EmptySpace emptySpace)
        {
            _buffer[emptySpace.Coordinate.Y][emptySpace.Coordinate.X] = new ColoredChar()
            {
                Character = ' '
            };
        }
    }
}