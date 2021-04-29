﻿using System;
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
        public bool _isValid = false;
        public Coordinate Coordinate { get; set; }
        bool ISudokuComponent.IsEditable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Cell Clone()
        {
            throw new NotImplementedException();
        }

        public bool IsComposite()
        {
            throw new NotImplementedException();
        }

        bool ISudokuComponent.IsComposite()
        {
            throw new NotImplementedException();
        }

        bool ISudokuComponent.IsValid()
        {
            return _isValid;
        }
    }
}