using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sudoku.Domain.Board
{
    public class CellLeaf : ISudokuComponent, IClone<CellLeaf>
    {
        public CellValue Value { get; set; }

        public bool _isValid = false;
        public Coordinate Coordinate { get; set; }

        bool ISudokuComponent.IsEditable
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public CellLeaf(Coordinate coordinate, int value)
        {
            Value = new CellValue(value);
            Coordinate = coordinate;
        }

        public CellLeaf Clone()
        {
            throw new NotImplementedException();
        }

        public bool IsComposite() => false;

        bool ISudokuComponent.IsValid()
        {
            return _isValid;
        }

        public IEnumerable<ISudokuComponent> GetChildren() =>
            new ReadOnlyCollection<ISudokuComponent>(new List<ISudokuComponent>());
    }
}