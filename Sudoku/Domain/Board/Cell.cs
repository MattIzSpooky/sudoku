using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Domain.Board.States;

namespace Sudoku.Domain.Board
{
    public class Cell : ICell, IClone<Cell>
    {
        public int Value {get;set;}
        public int HelpNumber { get; set; }
        public int IsValid { get; set; }
        public Coordinate Coordinate { get; set; }
        bool ICell.IsEditable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Cell Clone()
        {
            throw new NotImplementedException();
        }

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