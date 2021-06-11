using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Visitors;

namespace Domain.Board.Leaves
{
    public class WallLeaf : ISudokuComponent
    {
        public bool Horizontal { get; }
        public Coordinate Coordinate { get; }

        public WallLeaf(bool horizontal, Coordinate coordinate)
        {
            Horizontal = horizontal;
            Coordinate = coordinate;
        }

        public bool IsComposite() => false;
      

        public void Accept(ISudokuComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IEnumerable<ISudokuComponent> GetChildren() => new ReadOnlyCollection<ISudokuComponent>(new List<ISudokuComponent>());
    }
}