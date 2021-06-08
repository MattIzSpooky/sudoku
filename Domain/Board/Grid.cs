using System.Collections.Generic;
using Sudoku.Domain.Board.GridItems;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board
{
    public class Grid
    {
        private readonly List<IGridItem> _gridItems = new();
        public IReadOnlyList<IGridItem> GridItems => _gridItems;
        
        public int OffsetX { get; private set; }
        public int OffsetY { get; private set;}

        public void Add(IGridItem part)
        {
            _gridItems.Add(part);
        }

        public void SetOffsetX(int x)
        {
            OffsetX = x;
        }
        
        public void SetOffsetY(int y)
        {
            OffsetY = y;
        }

        public void Accept(IGridItemVisitor visitor)
        {
            foreach (var gridItem in _gridItems)
            {
                gridItem.Accept(visitor);
            }
        }
    }
}