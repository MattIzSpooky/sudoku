using System;
using System.IO;
using Domain.Parsers;
using NUnit.Framework;

namespace Tests
{
    public class SudokuReaderTests
    {
        private SudokuReader _reader;

        private const string FakeFilePath = @"./Levels/whatever.txt";
        private const string DummyFilePath = @"./Levels/puzzle.9x9-dummy";
        
        private const string FourByFourFilePath = @"./Levels/puzzle.4x4";
        private const string SixBySixFilePath = @"./Levels/puzzle.6x6";
        private const string NineByNineFilePath = @"./Levels/puzzle.9x9";
        private const string SamuraiFilePath = @"./Levels/puzzle.samurai";

        [SetUp]
        public void Setup()
        {
            _reader = new SudokuReader();
        }

        [Test]
        public void Read_WithEmptyString_ThrowsArgumentException()
        {
            Assert.Catch<ArgumentException>(() => _reader.Read(string.Empty));
        }
        
        [Test]
        public void Read_WithNullString_ThrowsArgumentException()
        {
            Assert.Catch<ArgumentException>(() => _reader.Read(null!));
        }
        
        [Test]
        public void Read_WithNotFoundFile_ThrowsFileNotFoundException()
        {
            Assert.Catch<FileNotFoundException>(() => _reader.Read(FakeFilePath));
        }
        
        [Test]
        public void Read_WithUnsupportedFile_ThrowsArgumentException()
        {
            Assert.Catch<ArgumentException>(() => _reader.Read(DummyFilePath));
        }

        [Test]
        public void Read_WithFourByFourPath_ShouldReturn4By4Sudoku()
        {
            var game = _reader.Read(FourByFourFilePath);

            Assert.True(game.Fields.Count == 1);
            Assert.True(game.Fields[0].Quadrants.Count == 4);
        }
        
        [Test]
        public void Read_WithSixBySixPath_ShouldReturn6By6Sudoku()
        {
            var game = _reader.Read(SixBySixFilePath);

            Assert.True(game.Fields.Count == 1);
            Assert.True(game.Fields[0].Quadrants.Count == 6);
        }
        
        [Test]
        public void Read_WithNineByNinePath_ShouldReturn9By9Sudoku()
        {
            var game = _reader.Read(NineByNineFilePath);
            
            Assert.True(game.Fields.Count == 1);
            Assert.True(game.Fields[0].Quadrants.Count == 9);
        }
        
        [Test]
        public void Read_WithSamuraiPath_ShouldReturnSamuraiSudoku()
        {
            var game = _reader.Read(SamuraiFilePath);
            
            Assert.True(game.Fields.Count == 5);
            Assert.True(game.Fields[0].Quadrants.Count == 9);
        }
    }
}