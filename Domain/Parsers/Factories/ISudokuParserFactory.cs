using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Parsers.Factories
{
    public interface ISudokuParserFactory
    {
        ISudokuParser CreateParser();
    }
}