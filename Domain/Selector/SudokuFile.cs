using Domain.Parsers;

namespace Domain.Selector
{
    public class SudokuFile
    {
        public string Name { get; }
        public bool IsSelected { get; set; }

        private readonly string _path;
        
        public SudokuFile(string name, string path)
        {
            Name = name;
            _path = path;
        }

        public Game Read() => new SudokuReader().Read(_path);
    }
}