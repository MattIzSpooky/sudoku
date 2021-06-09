using System;
using System.Collections.Generic;

namespace Sudoku.Domain.Board
{
    public class QuadrantComposite : ISudokuComponent
    {
        private readonly List<CellLeaf> _children;
        public IReadOnlyList<CellLeaf> Children => _children;

        public QuadrantComposite(List<CellLeaf>? cells)
        {
            _children = cells ?? new List<CellLeaf>();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISudokuComponent> GetChildren() => Children;

        public bool IsComposite() => true;

        public void AddCell(CellLeaf cellLeaf) => _children.Add(cellLeaf);
    }
}