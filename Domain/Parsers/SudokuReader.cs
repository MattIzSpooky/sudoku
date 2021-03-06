using System;
using System.IO;
using Domain.Parsers.Factories;

namespace Domain.Parsers
{
    public class SudokuReader
    {
        private readonly SudokuFactory _sudokuFactory = new();

        public SudokuReader()
        {
            RegisterSudokuTypes();
        }
        public Game Read(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path was null or empty.");
            
            if(!File.Exists(path))
                throw new FileNotFoundException("File doesn't exists");

            var content = File.ReadAllText(path);
            var parserFactory = _sudokuFactory.CreateFactory(Path.GetExtension(path));
            if (parserFactory == null)
                throw new ArgumentException("File not supported");
            
            var parser = parserFactory.CreateParser();
            
            var fields = parser.Parse(content);

            return new Game(fields);
        }

        private void RegisterSudokuTypes()
        {
            _sudokuFactory.RegisterType(".4x4", typeof(NormalSudokuParserFactory));
            _sudokuFactory.RegisterType(".6x6", typeof(NormalSudokuParserFactory));
            _sudokuFactory.RegisterType(".9x9", typeof(NormalSudokuParserFactory));
            _sudokuFactory.RegisterType(".jigsaw", typeof(JigsawSudokuParserFactory));
            _sudokuFactory.RegisterType(".samurai", typeof(SamuraiSudokuParserFactory));
        }
    }
}