using System.Collections.Generic;
using Sudoku.Domain.Board;

namespace Sudoku.Domain.Parsers
{
    public class Row
    {
        private List<ISudokuComponent> _components = new();
        public IReadOnlyList<ISudokuComponent> Components => _components;
        
        public void Add(ISudokuComponent component)
        {
            _components.Add(component);
        }
    }
}