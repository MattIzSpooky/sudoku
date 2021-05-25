using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return (ISudokuParserFactory) Activator.CreateInstance(_factories[type]);
        }
    }
}