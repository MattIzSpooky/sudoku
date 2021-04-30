using Sudoku.Domain.Parsers.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Parsers
{
    public class SudokuReader
    {
        private SudokuFactory _sudokuFactory = new SudokuFactory();

        SudokuReader()
        {
            _sudokuFactory.RegisterType(".4x4", typeof(NormalSudokuParserFactory));
        }

        public Game Read(string path)
        {
            throw new System.NotImplementedException();

            // Factory stuff
        }
    }
}