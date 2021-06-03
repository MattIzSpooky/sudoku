﻿using Sudoku.Domain.Board.GridItems;
using Sudoku.Domain.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Frontend.Visitors
{
    public class RenderVisitor : IGridItemVisitor<ColoredChar[][]>
    {
        private int _x;
        private int _y;
        public void Visit(ColoredChar[][] param, Wall wall)
        {
            param[_y][_x] =  new ColoredChar()
            {
                Character = wall.Horizontal ? '-' : '|'
            };
            _x++;
        }

        public void Visit(ColoredChar[][] param, Cell cell)
        {
            var character = char.Parse(cell.CellLeaf.Value.Value.ToString());
            
            param[_y][_x] =  new ColoredChar()
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
            param[_y][_x] = new ColoredChar()
            {
                Character = ' '
            };
            _x++;
        }
    }
}