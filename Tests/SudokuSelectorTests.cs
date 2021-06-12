using System.Linq;
using Domain.Selector;
using NUnit.Framework;

namespace Tests
{
    public class SudokuSelectorTests
    {
        private SudokuSelector _sudokuSelector;
        
        [SetUp]
        public void Setup()
        {
            _sudokuSelector = new SudokuSelector();
            _sudokuSelector.ReadFromDisk();
        }

        [Test]
        public void GetSelected_WhenInitialized_ShouldReturnFirstValue()
        {
            var selected = _sudokuSelector.GetSelected();
            
            Assert.AreSame(_sudokuSelector.SudokuFiles[0], selected);
        }
        
        [Test]
        public void Next_CalledTwice_ShouldReturnThirdValue()
        {
            _sudokuSelector.Next();
            _sudokuSelector.Next();
            
            Assert.AreSame(_sudokuSelector.SudokuFiles[2], _sudokuSelector.GetSelected());
        }

        [Test]
        public void Previous_WhenJustInitialized_SelectedShouldRemainAtFirst()
        {
            _sudokuSelector.Previous();
            
            Assert.AreSame(_sudokuSelector.SudokuFiles[0], _sudokuSelector.GetSelected());
        }
        
        [Test]
        public void Previous_WhenCalledNext_SelectedShouldBeBackAtFirst()
        {
            _sudokuSelector.Next();
            _sudokuSelector.Previous();
            
            Assert.AreSame(_sudokuSelector.SudokuFiles[0], _sudokuSelector.GetSelected());
        }

        [Test]
        public void Next_WhenExcessivelyCalled_ShouldReturnLast()
        {
            for (var i = 0; i < _sudokuSelector.SudokuFiles.Count * 2; i++)
            {
                _sudokuSelector.Next();
            }
            
            Assert.AreSame(_sudokuSelector.SudokuFiles[^1], _sudokuSelector.GetSelected());
        }
        
        [Test]
        public void Previous_WhenExcessivelyCalled_ShouldReturnFirst()
        {
            for (var i = 0; i < _sudokuSelector.SudokuFiles.Count * 2; i++)
            {
                _sudokuSelector.Previous();
            }
            
            Assert.AreSame(_sudokuSelector.SudokuFiles[0], _sudokuSelector.GetSelected());
        }

        [Test]
        public void Read_AlwaysReturnsGame()
        {
            var selected = _sudokuSelector.GetSelected();
            var game = selected.Read();
            
            Assert.NotNull(game);
        }
    }
}