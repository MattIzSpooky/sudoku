using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sudoku.Domain.Selector
{
    public class SudokuSelector
    {
        private readonly List<SudokuFile> _sudokuFiles = new();

        public IReadOnlyList<SudokuFile> SudokuFiles => _sudokuFiles;

        public void ReadFromDisk()
        {
            var filePaths = Directory.GetFiles(@"./Frontend/Levels");

            foreach (var path in filePaths)
            {
                var pos = path.LastIndexOf("/", StringComparison.Ordinal) + 1;

                _sudokuFiles.Add(new SudokuFile(path[pos..], path));
            }

            _sudokuFiles[0].IsSelected = true;
        }

        private int GetIndexOfSudokuFile(SudokuFile sudokuFile) => _sudokuFiles.FindIndex(a => a.Equals(sudokuFile));

        private void ChangeSelection(int indexDirection)
        {
            var selected = GetSelected();
            var index = GetIndexOfSudokuFile(selected) + indexDirection;

            var nextSudoku = _sudokuFiles.ElementAtOrDefault(index);
            if (nextSudoku == null) return;

            nextSudoku.IsSelected = true;
            selected.IsSelected = false;
        }

        public void Previous() => ChangeSelection(-1);

        public void Next() => ChangeSelection(1);

        public SudokuFile GetSelected() => _sudokuFiles.First(sudokuFile => sudokuFile.IsSelected);
    }
}