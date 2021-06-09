using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board.Leaves
{
    public class CellLeaf : ISudokuComponent
    {
        public CellValue Value { get; set; }
        
        public bool IsValid { get; set; }
        public Coordinate Coordinate { get; set; }
        
        public bool IsLocked { get; init; }

        public CellLeaf(Coordinate coordinate, int value)
        {
            Value = new CellValue(value);
            Coordinate = coordinate;
            IsValid = true;
        }

        public bool IsComposite() => false;

        public void Accept(ISudokuComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IEnumerable<ISudokuComponent> GetChildren() =>
            new ReadOnlyCollection<ISudokuComponent>(new List<ISudokuComponent>());
    }
}