using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Domain.Board.States;

namespace Sudoku.Domain.Board
{
    public class Cell : ISudokuComponent, IClone<Cell>
    {
        public int Value {get;set;}
        public int HelpNumber { get; set; }
        public int IsValid { get; set; }
        public Coordinate Coordinate { get; set; }
        bool ISudokuComponent.IsEditable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Cell(Coordinate coordinate, int value)
        {
            Value = value;
            Coordinate = coordinate;
        }
        
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