using Domain;
using Domain.Board;
using Domain.Board.Leaves;
using Domain.States;
using NUnit.Framework;

namespace Tests
{
    public class DefinitiveStateTests
    {
        private Game _game;
        private DefinitiveState _definitiveState;
                
        [SetUp]
        public void Setup()
        {
            _game = GameData.Game;
            _definitiveState = new DefinitiveState();
        }

        [Test]
        public void Handle_NewDefinitiveValueToCellLeaf_ShouldSetDefinitiveValueToCellLeaf()
        {
            var cellLeaf = new CellLeaf(new Coordinate(0, 0), 0) {IsValid = true};

            _definitiveState.Handle(cellLeaf,5);
            
            Assert.AreEqual(cellLeaf.Value.DefinitiveValue,5);
        }
        
        [Test]
        public void Handle_SameDefinitiveValueToCellLeaf_ShouldSetDefinitiveValueToZero()
        {
            var cellLeaf = new CellLeaf(new Coordinate(0, 0), 2) {IsValid = true};

            _definitiveState.Handle(cellLeaf,2);
            
            Assert.AreEqual(cellLeaf.Value.DefinitiveValue,0);
        }

        [Test]
        public void ChangeState_TransitionToAuxiliaryState_ShouldChangeStateToAuxiliaryState()
        {
            var auxiliaryState = new AuxiliaryState();
            _game.TransitionTo(_definitiveState);
            
            _definitiveState.ChangeState();

            Assert.AreNotEqual(_game.GetStateName(), _definitiveState.GetName());
            Assert.AreEqual(_game.GetStateName(), auxiliaryState.GetName());
        }
        
        [Test]
        public void SwitchState_CalledTwice_ShouldChangeStateToDefinitiveState()
        {
            var auxiliaryState = new AuxiliaryState();
            _game.TransitionTo(_definitiveState);
            
            _game.SwitchState();
            _game.SwitchState();
            
            Assert.AreEqual(_game.GetStateName(), _definitiveState.GetName());
            Assert.AreNotEqual(_game.GetStateName(), auxiliaryState.GetName());
        }
    }
}