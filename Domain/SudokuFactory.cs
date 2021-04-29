using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SudokuFactory
    {
        private Dictionary<string, Type> _factories;

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