using System;
using System.IO;
using Sudoku.Domain.Parsers.Factories;

namespace Sudoku.Domain.Parsers
{
    public class SudokuReader
    {
        private readonly SudokuFactory _sudokuFactory = new();
        public Game Read(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path was null or empty.");
            
            if(!File.Exists(path))
                throw new ArgumentException("File doesn't exists");

            var content = File.ReadAllText(path);

            return new Game();
        }
    }
}