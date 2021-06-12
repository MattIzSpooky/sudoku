using Domain.Board;
using Domain.Board.Leaves;
using Domain.Visitors;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class SudokuComponentVisitorTests
    {
        private Mock<ISudokuComponentVisitor> _visitor;

        [SetUp]
        public void Setup()
        {
            _visitor = new Mock<ISudokuComponentVisitor>();
        }

        [Test]
        public void Visit_ShouldVisit_CellLeaf()
        {
            _visitor.Setup(v => v.Visit(It.IsAny<CellLeaf>())).Verifiable();

            var cell = new CellLeaf(new Coordinate(), 0);
            cell.Accept(_visitor.Object);

            Assert.DoesNotThrow(() => _visitor.Verify());
        }

        [Test]
        public void Visit_ShouldVisit_WallLeaf()
        {
            _visitor.Setup(v => v.Visit(It.IsAny<WallLeaf>())).Verifiable();

            var wall = new WallLeaf(false, new Coordinate());
            wall.Accept(_visitor.Object);

            Assert.DoesNotThrow(() => _visitor.Verify());
        }

        [Test]
        public void Visit_ShouldVisit_QuadrantComposite()
        {
            _visitor.Setup(v => v.Visit(It.IsAny<QuadrantComposite>())).Verifiable();

            var quadrantComposite = new QuadrantComposite();
            quadrantComposite.Accept(_visitor.Object);

            Assert.DoesNotThrow(() => _visitor.Verify());
        }
    }
}