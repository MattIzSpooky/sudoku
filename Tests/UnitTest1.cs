using Domain;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private Game _game;
        
        [SetUp]
        public void Setup()
        {
            _game = GameData.Game;
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        
        [Test]
        public void Test2()
        {
            Assert.Pass();
        }
    }
}