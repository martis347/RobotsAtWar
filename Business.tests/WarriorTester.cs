using log4net;
using log4net.Config;
using NUnit.Framework;

namespace Business.tests
{
    [TestFixture]
    public class WarriorTester
    {
        private Warrior _warrior;
        //private ILog _testLogger;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
        }

        [SetUp]
        public void Setup()
        {
            _warrior = new Warrior(10);
        }
        [Test]
        [TestCase(0, 11)]
        [TestCase(1, 11)]
        [TestCase(3, 14)]
        public void TestRest(int time, int expectation)
        {
            _warrior.Rest(time);
            Assert.AreEqual(expectation,_warrior.Check().Life);
        }


    }
}
