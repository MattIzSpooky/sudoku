using System.Collections.Generic;
using Sudoku.Domain.Board.GridItems;

namespace Sudoku.Domain.Board
{
    public class Grid
    {
        private readonly List<IGridItem> _gridItems = new();
        public IReadOnlyList<IGridItem> GridItems => _gridItems;

        public void Add(IGridItem part)
        {
            _gridItems.Add(part);
        }
    }
}