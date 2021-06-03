using Sudoku.Domain.Board;
using Sudoku.Domain.Board.GridItems;
using Sudoku.Domain.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Frontend.Visitors
{
    public class RenderVisitor : IGridItemVisitor<ColoredChar[][]>
    {
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public Coordinate Cursor { get; set; }
        
        private int _x;
        private int _y;
        public void Visit(ColoredChar[][] param, Wall wall)
        {
            param[_y + OffsetY][_x + OffsetX] =  new ColoredChar()
            {
                Character = wall.Horizontal ? '-' : '|'
            };
            _x++;
        }

        public void Visit(ColoredChar[][] param, Cell cell)
        {
            var value = cell.CellLeaf.Value.Value;
            var character = value == 0 ? ' ' : char.Parse(value.ToString());
            
            param[_y + OffsetY][_x + OffsetX] =  new ColoredChar()
            {
                Character = character
            };
            _x++;
        }

        public void Visit(ColoredChar[][] param, Row row)
        {
            _y++;
            _x = 0;
        }

        public void Visit(ColoredChar[][] param, EmptySpace emptySpace)
        {
            param[_y + OffsetY][_x + OffsetX] = new ColoredChar()
            {
                Character = ' '
            };
            _x++;
        }
    }
}