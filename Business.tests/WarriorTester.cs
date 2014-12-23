using System.Threading;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using NUnit.Framework;

namespace Business.tests
{
    [TestFixture]
    public class WarriorTester
    {
        private Warrior _warrior;

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
        [TestCase(10, 10)]
        [TestCase(-1, 1)]
        [TestCase(100, 100)]
        public void TestWarriorConstructor(int life, int expectation)
        {
            Warrior testWarrior = new Warrior(life);
            Assert.AreEqual(expectation, testWarrior.Check().Life);
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

        [Test]
        [TestCase(Action.Attack, State.Attacking)]
        [TestCase(Action.Defend, State.Defending)]
        [TestCase(Action.Rest, State.Resting)]
        public void TestCheck(Action action, State expectation)
        {
            Command command;
            command.Action = action;
            command.Time = 2;
            Parallel.Invoke(
                () =>
                {
                    _warrior.SetCommand(command);
                },

                () =>
                {
                    Thread.Sleep(500);
                    Assert.AreEqual(_warrior.Check().State, expectation);
                }
            );
        }
    }
}
