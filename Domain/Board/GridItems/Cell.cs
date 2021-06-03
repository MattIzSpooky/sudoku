﻿using System.Text;
using Sudoku.Domain.Visitors;
using Sudoku.Mvc.Views.Console;

namespace Sudoku.Domain.Board.GridItems
{
    public class Cell : IGridItem
    {
        public CellLeaf CellLeaf { get; }
        public Cell(CellLeaf cellLeaf)
        {
            CellLeaf = cellLeaf;
        }
        
        public void Accept(ColoredChar[][] param, IGridItemVisitor<ColoredChar[][]> visitor)
        {
            visitor.Visit(param, this);
        }
    }
}