using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Extensions;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board
{
    public class Field : ISudokuComponent
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
        
        
        public Field(List<QuadrantComposite> quadrants, int maxValue, int offsetX = 0, int offsetY = 0)
        {
            _quadrants = quadrants;

            OffsetX = offsetX;
            OffsetY = offsetY;

            MaxValue = maxValue;
        }
        
        public void Validate()
        {
            var cells = GetChildren().SelectMany(q => q.GetChildren()).Cast<CellLeaf>().ToList();
                
            foreach (var cell in cells)
            {
                cell.IsValid = true;
                    
                if (cell.IsLocked || cell.Value.Value == 0) continue;

                var rowColumn = cells
                    .Where(c => (c.Coordinate.Y == cell.Coordinate.Y || c.Coordinate.X == cell.Coordinate.X) &&
                                c != cell)
                    .FirstOrDefault(c => c.Value.Value == cell.Value.Value);

                if (rowColumn != null) cell.IsValid = false;

                var quadrant = Find(c => c.GetChildren().Contains(cell)).First();

                if (quadrant.GetChildren().Cast<CellLeaf>()
                    .FirstOrDefault(c => c.Value.Value == cell.Value.Value && c != cell) != null) cell.IsValid = false;
            }
        }
    }
}