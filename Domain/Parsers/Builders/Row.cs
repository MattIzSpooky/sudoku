using System.Collections.Generic;
using Domain.Board;

namespace Domain.Parsers.Builders
{
    public class Row
    {
        private readonly List<ISudokuComponent> _components = new();
        public IReadOnlyList<ISudokuComponent> Components => _components;

        public void Add(ISudokuComponent component) => _components.Add(component);
    }
}