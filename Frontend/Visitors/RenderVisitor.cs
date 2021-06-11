using System.Drawing;
using Domain.Board;
using Domain.Board.Leaves;
using Domain.Visitors;
using MVC.Views.Console;

namespace Frontend.Visitors
{
    public class RenderVisitor : ISudokuComponentVisitor
    {

        private readonly ColoredChar[][] _buffer;

        public RenderVisitor(ColoredChar[][] buffer)
        {
            _buffer = buffer;
        }
        
        public void Visit(WallLeaf wallLeaf)
        {
            _buffer[wallLeaf.Coordinate.Y][wallLeaf.Coordinate.X] = new ColoredChar()
            {
                Character = wallLeaf.Horizontal ? '-' : '|'
            };
        }

        public void Visit(CellLeaf cell)
        {
            char character;
            var cellLeafValue = cell.Value;
            var definitiveValue = cellLeafValue.DefinitiveValue;
            
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

        public void Visit(QuadrantComposite quadrantComposite)
        {
            foreach (var child in quadrantComposite.Children)
            {
                child.Accept(this);
            }
        }
    }
}