using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Board
{
    public class QuadrantComposite : ISudokuComponent, IClone<QuadrantComposite>
    {
        private readonly List<CellLeaf> _children;
        public IReadOnlyList<CellLeaf> Children => _children;
        bool ISudokuComponent.IsEditable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public QuadrantComposite(List<CellLeaf> cells)
        {
            _children = cells ?? new List<CellLeaf>();
        }
        
        public QuadrantComposite Clone()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISudokuComponent> GetChildren() => Children;

        bool ISudokuComponent.IsComposite()
        {
            return true;
        }

        bool ISudokuComponent.IsValid()
        {
            throw new NotImplementedException();
        }

        public void AddCell(CellLeaf cellLeaf) => _children.Add(cellLeaf);
    }
}