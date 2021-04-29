using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Quadrant : ICell, IClone<Quadrant>
    {
        private List<Cell> cells;
        bool ICell.IsEditable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Quadrant Clone()
        {
            throw new NotImplementedException();
        }

        public bool IsComposite()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        bool ICell.IsComposite()
        {
            throw new NotImplementedException();
        }

        bool ICell.IsValid()
        {
            throw new NotImplementedException();
        }
    }
}