namespace Domain.Selector
{
    public class SudokuFile
    {
        public string Name { get; }
        public string Path { get; }
        public bool IsSelected { get; set; }

        public SudokuFile(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}