using System.Linq;
using Domain.Board;
using Domain.Board.Leaves;
using Domain.Parsers;
using NUnit.Framework;

namespace Tests.ISudokuParser
{
    public class NormalSudokuParserTests
    {
        private NormalSudokuParser _parser;

        private const int AmountOfFields = 1;

        private const string FourByFourSudoku = "0340400210030210";
        private const string SixBySixSudoku = "003010560320054203206450012045040100";
        private const string NineByNineSudoku =
            "700509001000000000150070063003904100000050000002106400390040076000000000600201004";

        [SetUp]
        public void Setup()
        {
            _parser = new NormalSudokuParser();
        }

        [Test]
        [TestCase(FourByFourSudoku)]
        [TestCase(SixBySixSudoku)]
        [TestCase(NineByNineSudoku)]
        public void Parse_WithNormalSudoku_ShouldReturnOneField(string sudoku)
        {
            var fields = _parser.Parse(sudoku);

            Assert.True(fields.Length == AmountOfFields);
        }

        [Test]
        [TestCase(FourByFourSudoku)]
        [TestCase(SixBySixSudoku)]
        [TestCase(NineByNineSudoku)]
        public void Parse_WithNormalSudoku_ShouldHaveAmountOfCellsEqualToSudokuStringLength(string sudoku)
        {
            var fields = _parser.Parse(sudoku);
            var cells = fields[0].Quadrants.SelectMany(q => q.Cells);

            Assert.AreEqual(sudoku.Length, cells.Count());
        }

        [Test]
        [TestCase(FourByFourSudoku, 4)]
        [TestCase(SixBySixSudoku, 6)]
        [TestCase(NineByNineSudoku, 9)]
        public void Parse_WithNormalSudoku_ShouldHaveNQuadrants(string sudoku, int amountOfQuadrants)
        {
            var fields = _parser.Parse(sudoku);

            Assert.AreEqual(amountOfQuadrants, fields[0].Quadrants.Count);
        }

        [Test]
        [TestCase(FourByFourSudoku, 4)]
        [TestCase(SixBySixSudoku, 6)]
        [TestCase(NineByNineSudoku, 9)]
        public void Parse_WithNormalSudoku_ShouldHaveNCellsPerQuadrant(string sudoku, int amountOfCellsPerQuadrant)
        {
            var fields = _parser.Parse(sudoku);
            
            foreach (var quadrant in fields[0].Quadrants)
            {
                Assert.AreEqual(amountOfCellsPerQuadrant, quadrant.Cells.Count);
            }
        }

        [Test]
        public void Parse_WithNormalSudoku_ShouldHaveCorrectCoordinates()
        {
            var fields = _parser.Parse(FourByFourSudoku);

            var firstQuadrant = fields[0].Quadrants[0];
            var lastQuadrant = fields[0].Quadrants[^1];
            
            Assert.AreEqual(new Coordinate(0, 0), firstQuadrant.Cells[0].Coordinate);
            Assert.AreEqual(new Coordinate(1, 0), firstQuadrant.Cells[1].Coordinate);
            Assert.AreEqual(new Coordinate(0, 1), firstQuadrant.Cells[2].Coordinate);
            Assert.AreEqual(new Coordinate(1, 1), firstQuadrant.Cells[3].Coordinate);
            
            Assert.AreEqual(new Coordinate(3, 3), lastQuadrant.Cells[0].Coordinate);
            Assert.AreEqual(new Coordinate(4, 3), lastQuadrant.Cells[1].Coordinate);
            Assert.AreEqual(new Coordinate(3, 4), lastQuadrant.Cells[2].Coordinate);
            Assert.AreEqual(new Coordinate(4, 4), lastQuadrant.Cells[3].Coordinate);
        }
        
        [Test]
        public void Parse_WithNormalSudoku_ShouldHaveCorrectValues()
        {
            var fields = _parser.Parse(FourByFourSudoku);

            var firstQuadrant = fields[0].Quadrants[0];
            var lastQuadrant = fields[0].Quadrants[^1];
            
            Assert.AreEqual(0, firstQuadrant.Cells[0].Value.DefinitiveValue);
            Assert.AreEqual(3, firstQuadrant.Cells[1].Value.DefinitiveValue);
            Assert.AreEqual(4, firstQuadrant.Cells[2].Value.DefinitiveValue);
            Assert.AreEqual(0, firstQuadrant.Cells[3].Value.DefinitiveValue);
            
            Assert.AreEqual(0, lastQuadrant.Cells[0].Value.DefinitiveValue);
            Assert.AreEqual(3, lastQuadrant.Cells[1].Value.DefinitiveValue);
            Assert.AreEqual(1, lastQuadrant.Cells[2].Value.DefinitiveValue);
            Assert.AreEqual(0, lastQuadrant.Cells[3].Value.DefinitiveValue);
        }
        
        [Test]
        [TestCase(FourByFourSudoku)]
        [TestCase(SixBySixSudoku)]
        [TestCase(NineByNineSudoku)]
        public void Parse_WithNormalSudoku_ShouldHaveNWalls(string sudoku)
        {
            var fields = _parser.Parse(FourByFourSudoku);

            var actualAmountOfWalls = fields[0].Quadrants.SelectMany(q => q.Children).OfType<WallLeaf>().Count();
            
            
            Assert.AreEqual(9, actualAmountOfWalls);
        }
    }
}