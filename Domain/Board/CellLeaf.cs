using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sudoku.Domain.Board
{
    public class CellLeaf : ISudokuComponent, IClone<CellLeaf>
    {
        public CellValue Value { get; set; }
        
        public bool IsValid { get; set; }
        public Coordinate Coordinate { get; set; }
        
        public bool IsLocked { get; private set; }

        bool ISudokuComponent.IsEditable
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public CellLeaf(Coordinate coordinate, int value, bool locked = false)
        {
            Value = new CellValue(value);
            Coordinate = coordinate;
            IsValid = true;
            IsLocked = locked;
        }

        public CellLeaf Clone()
        {
            throw new NotImplementedException();
        }

        public bool IsComposite() => false;

        bool ISudokuComponent.IsValid()
        {
            return IsValid;
        }

        public IEnumerable<ISudokuComponent> GetChildren() =>
            new ReadOnlyCollection<ISudokuComponent>(new List<ISudokuComponent>());
    }
}