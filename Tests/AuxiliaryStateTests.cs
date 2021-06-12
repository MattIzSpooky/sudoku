using Domain;
using Domain.Board;
using Domain.Board.Leaves;
using Domain.States;
using NUnit.Framework;

namespace Tests
{
    public class AuxiliaryStateTests
    {
        private Game _game;
        private AuxiliaryState _auxiliaryState;
                
        [SetUp]
        public void Setup()
        {
            _game = GameData.Game;
            _auxiliaryState = new AuxiliaryState();
        }

        [Test]
        public void Handle_NewAuxiliaryValueToCellLeaf_ShouldSetAuxiliaryValueToCellLeaf()
        {
            var cellLeaf = new CellLeaf(new Coordinate(0, 0), 0) {IsValid = true};

            _auxiliaryState.Handle(cellLeaf,5);
            
            Assert.AreEqual(cellLeaf.Value.HelpNumber,5);
        }
        
        [Test]
        public void Handle_NewAuxiliaryValueToDefinitiveCellLeaf_ShouldNotSetAuxiliaryValue()
        {
            var cellLeaf = new CellLeaf(new Coordinate(0, 0), 9) {IsValid = true};

            _auxiliaryState.Handle(cellLeaf,9);
            
            Assert.AreEqual(cellLeaf.Value.HelpNumber,0);
            Assert.AreEqual(cellLeaf.Value.DefinitiveValue,9);
        }
        
        [Test]
        public void Handle_SameAuxiliaryValueToCellLeaf_ShouldSetAuxiliaryValueToZero()
        {
            var cellLeaf = new CellLeaf(new Coordinate(0, 0), 0) {IsValid = true};
            
            _auxiliaryState.Handle(cellLeaf,2);
            _auxiliaryState.Handle(cellLeaf,2);
            
            Assert.AreEqual(cellLeaf.Value.DefinitiveValue,0);
            Assert.AreEqual(cellLeaf.Value.HelpNumber,0);
        }
        
        [Test]
        public void ChangeState_TransitionToDefinitiveState_ShouldChangeStateToDefinitiveState()
        {
            var definitiveState = new DefinitiveState();
            _game.TransitionTo(_auxiliaryState);
            
            _auxiliaryState.ChangeState();

            Assert.AreNotEqual(_game.GetStateName(), _auxiliaryState.GetName());
            Assert.AreEqual(_game.GetStateName(), definitiveState.GetName());
        }
        
        [Test]
        public void SwitchState_CalledTwice_ShouldChangeStateToAuxiliaryState()
        {
            var definitiveState = new DefinitiveState();
            _game.TransitionTo(_auxiliaryState);
            
            _game.SwitchState();
            _game.SwitchState();
            
            Assert.AreEqual(_game.GetStateName(), _auxiliaryState.GetName());
            Assert.AreNotEqual(_game.GetStateName(), definitiveState.GetName());
        }
    }
}