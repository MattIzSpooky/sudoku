using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Cell : ICell
    {
        public int Value {get;set;}
        public int HelpNumber { get; set; }
        public int IsValid { get; set; }
        public Coordinate Coordinate { get; set; }
        bool ICell.IsEditable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsComposite()
        {
            throw new NotImplementedException();
        }

        public void SetState(State state)
        {
            throw new System.NotImplementedException();
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