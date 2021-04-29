using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Board
{
    public class Quadrant : ISudokuComponent, IClone<Quadrant>
    {
        private List<Cell> cells;
        bool ISudokuComponent.IsEditable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
    }
}