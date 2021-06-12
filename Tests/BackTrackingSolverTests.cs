using Domain;
using Domain.Solvers;
using NUnit.Framework;

namespace Tests
{
    public class BackTrackingSolverTests
    {
        private Game _game;
        private BackTrackingSolver _backTrackingSolver;

        [SetUp]
        public void Setup()
        {
            _game = GameData.Game;
            _backTrackingSolver = new BackTrackingSolver();
        }

        [Test]
        public void Solve_HalfEmptyBoard_ShouldSolveAllCellsCorrectly()
        {
            _backTrackingSolver.Solve(_game.Fields[0]);
            
            // Upper left Quadrant
            Assert.AreEqual(_game.Fields[0].Quadrants[0].Cells[0].Value.DefinitiveValue,2);
            Assert.AreEqual(_game.Fields[0].Quadrants[0].Cells[3].Value.DefinitiveValue,1);
            // Upper right Quadrant
            Assert.AreEqual(_game.Fields[0].Quadrants[1].Cells[1].Value.DefinitiveValue,1);
            Assert.AreEqual(_game.Fields[0].Quadrants[1].Cells[2].Value.DefinitiveValue,3);
            // Down left Quadrant
            Assert.AreEqual(_game.Fields[0].Quadrants[2].Cells[1].Value.DefinitiveValue,4);
            Assert.AreEqual(_game.Fields[0].Quadrants[2].Cells[2].Value.DefinitiveValue,3);
            // Down right Quadrant
            Assert.AreEqual(_game.Fields[0].Quadrants[3].Cells[0].Value.DefinitiveValue,2);
            Assert.AreEqual(_game.Fields[0].Quadrants[3].Cells[3].Value.DefinitiveValue,4);
        }
    }
}