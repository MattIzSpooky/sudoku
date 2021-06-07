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
            param[wall.Y][wall.X] =  new ColoredChar()
            {
                Character = wall.Horizontal ? '-' : '|'
            };
        }

        public void Visit(ColoredChar[][] param, Cell cell)
        {
            var value = cell.CellLeaf.Value.Value;
            var character = value == 0 ? ' ' : char.Parse(value.ToString());
            
            param[cell.Y][cell.X] =  new ColoredChar()
            {
                Character = character
            };
        }

        public void Visit(ColoredChar[][] param, Row row)
        {
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