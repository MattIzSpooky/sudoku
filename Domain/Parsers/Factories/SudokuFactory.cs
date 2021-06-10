using System;
using System.Collections.Generic;

namespace Sudoku.Domain.Parsers.Factories
{
    public class SudokuFactory
    {
        private readonly Dictionary<string, Type> _factories = new();

        public void RegisterType(string name, Type type)
        {
            _factories[name] = type;
        }

        public ISudokuParserFactory CreateFactory(string type)
        {
            if (!_factories.ContainsKey(type))
                return null;
            
            return (ISudokuParserFactory) Activator.CreateInstance(_factories[type]);
        }
    }
}