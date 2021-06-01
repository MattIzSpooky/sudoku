using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Parsers.Factories
{
    public class SamuraiSudokuParserFactory : ISudokuParserFactory
    {
        public ISudokuParser CreateParser()
        {
            return new SamuraiSudokuParser();
        }
    }
}