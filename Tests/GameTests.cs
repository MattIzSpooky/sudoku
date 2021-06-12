using System.Linq;
using Domain;
using NUnit.Framework;

namespace Tests
{
    public class GameTests
    {
        private Game _game;
        
        [SetUp]
        public void Setup()
        {
            _game = GameData.Game;
        }

        [Test]
        public void EnterValue_ValueMoreThanMax_ShouldNotSetValueOnCell()
        {
            _game.MoveCursor(0,0);
            
            _game.EnterValue(500);

            var selectedCell = _game.Fields[0].Quadrants[0].Cells[0];

            Assert.AreEqual(selectedCell.Value.DefinitiveValue, 0);
            Assert.AreEqual(selectedCell.Value.HelpNumber, 0);
        }
        
        [Test]
        public void EnterValue_ValueLessThanMax_ShouldSetValueOnCell()
        {
            _game.MoveCursor(0,0);
            
            _game.EnterValue(1);

            var selectedCell = _game.Fields[0].Quadrants[0].Cells[0];

            Assert.AreEqual(selectedCell.Value.DefinitiveValue, 1);
        }
        
        [Test]
        public void EnterValue_ValueIsLocked_ShouldNotSetValueOnCell()
        {
            _game.MoveCursor(1,0);
            
            _game.EnterValue(1);

            var selectedCell = _game.Fields[0].Quadrants[0].Cells[1];

            Assert.AreEqual(selectedCell.Value.DefinitiveValue, 3);
        }
        
        [Test]
        public void MoveCursor_ValueMoreThanMax_ShouldNotSetCursorValue()
        {
            _game.MoveCursor(0,0);
            
            _game.MoveCursor(90,105);

            Assert.AreEqual(_game.Cursor.X, 0);
            Assert.AreEqual(_game.Cursor.Y, 0);
        }
        
        [Test]
        public void MoveCursor_ValueLessThanMax_ShouldSetCursorValue()
        {
            _game.MoveCursor(1,2);

            Assert.AreEqual(_game.Cursor.X, 1);
            Assert.AreEqual(_game.Cursor.Y, 2);
        }
        
        [Test]
        public void Solve_AllFields_AllShouldCellsNotEmpty()
        { 
            _game.Solve();

            var cellEmpty = _game.Fields[0].Quadrants.Any(q => q.Cells.Any(c => c.Value.DefinitiveValue == 0));

            Assert.IsFalse(cellEmpty);
        }
        
        [Test]
        public void Validate_InValidValue_CellShouldInValid()
        {
            var cell = _game.Fields[0].Quadrants[0].Cells[0];
            cell.Value.DefinitiveValue = 1;
            
            _game.Validate();

            Assert.IsFalse(cell.IsValid);
        }
        
        [Test]
        public void Validate_ValidValue_CellShouldValid()
        {
            var cell = _game.Fields[0].Quadrants[0].Cells[0];
            cell.Value.DefinitiveValue = 2;
            
            _game.Validate();

            Assert.IsTrue(cell.IsValid);
        }
    }
}