﻿using Sudoku.Domain.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Domain.Board.GridItems
{
    public class Wall : IGridItem
    {
        public bool Horizontal { get; }

        public Wall(bool horizontal)
        {
            Horizontal = horizontal;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public void Accept(ColoredChar[][] param, IGridItemVisitor<ColoredChar[][]> visitor)
        {
            visitor.Visit(param, this);
        }
    }
}