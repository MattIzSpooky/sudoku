using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SudokuParserFactory
    {
        private Dictionary<string, Type> _parsers;

        public ISudokuParser CreateParser(string type)
        {
            throw new System.NotImplementedException();
        }
    }
}