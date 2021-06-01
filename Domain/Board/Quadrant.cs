using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Board
{
    public class Quadrant : ISudokuComponent, IClone<Quadrant>
    {
        private readonly List<Cell> _cells;
        public IReadOnlyList<Cell> Cells => _cells;
        bool ISudokuComponent.IsEditable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Quadrant(List<Cell> cells)
        {
            _cells = cells ?? new List<Cell>();
        }
        
        public Quadrant Clone()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        bool ISudokuComponent.IsComposite()
        {
            throw new NotImplementedException();
        }

        bool ISudokuComponent.IsValid()
        {
            throw new NotImplementedException();
        }

        public void AddCell(Cell cell) => _cells.Add(cell);
    }
}