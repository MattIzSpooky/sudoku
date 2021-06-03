using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Extensions;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board
{
    public class Sudoku : ISudokuComponent
    {
        private readonly List<QuadrantComposite> _quadrants;
        private bool _isEditable;

        public int OffsetX { get; }
        public int OffsetY { get; }
        public int MaxValue { get; }

        public IReadOnlyList<QuadrantComposite> Quadrants => _quadrants;

        public List<CellLeaf> GetOrderedCells()
        {
            return Find(c => !c.IsComposite() && c is CellLeaf)
                .Cast<CellLeaf>()
                .OrderBy(c => c.Coordinate.Y)
                .ThenBy(c => c.Coordinate.X)
                .ToList();
        }

        public IEnumerable<ISudokuComponent> Find(Func<ISudokuComponent, bool> finder)
        {
            return GetChildren().Descendants(i => i.GetChildren()).Where(finder);
        }

        bool ISudokuComponent.IsEditable
        {
            get => _isEditable;
            set => _isEditable = value;
        }

        public bool IsComposite() => true;

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        IEnumerable<ISudokuComponent> ISudokuComponent.GetChildren()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISudokuComponent> GetChildren()
        {
            return _quadrants;
        }

        public Grid Accept(ISudokuVisitor visitor)
        {
            return visitor.Visit(this);
        }
        
        
        public Sudoku(List<QuadrantComposite> quadrants, int maxValue, int offsetX = 0, int offsetY = 0)
        {
            _quadrants = quadrants;

            OffsetX = offsetX;
            OffsetY = offsetY;

            MaxValue = maxValue;
        }
    }
}