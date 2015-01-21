using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Business.Enums;
using log4net.Config;
using NUnit.Framework;

namespace Business.Tests
{
    [TestFixture]
    public class WarriorTester
    {
        private const int CLIFE = 100;
        private Warrior _warrior;
        private Warrior _warrior2;


        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            XmlConfigurator.Configure();
        }

        [SetUp]
        public void SetUp()
        {
            _warrior = new Warrior(new FakeTimeMachine(), 100);
            _warrior2 = new Warrior(new FakeTimeMachine(), 100);
        }
        [Test]
        [TestCase(Strength.None, State.Attacking, State.Attacking, State.Interrupted)]
        [TestCase(Strength.Weak, State.Attacking, State.Attacking, State.Interrupted)]
        [TestCase(Strength.Normal, State.Attacking, State.Attacking, State.Interrupted)]
        [TestCase(Strength.Strong, State.Attacking, State.Attacking, State.Interrupted)]
        public void AttackCheck(Strength str, State state,State expected,State expectedEnemy)
        {
            _warrior.Attack(str);
            Assert.AreEqual(expected,_warrior.CheckMe().State);
        }

        [Test]
        [TestCase(Strength.None, CLIFE, State.Interrupted)]
        [TestCase(Strength.Weak, CLIFE - 1, State.Interrupted)]
        [TestCase(Strength.Normal, CLIFE - 2, State.Interrupted)]
        [TestCase(Strength.Strong, CLIFE - 4, State.Interrupted)]
        public void GetAttackedCheck(Strength str,int expectedLife, State expectedState)
        {
           // _warrior.GetAttacked(str);
            Assert.AreEqual(expectedLife,_warrior.CheckMe().Life);
            Assert.AreEqual(expectedState, _warrior.CheckMe().State);
        }

        [Test]
        [TestCase(Strength.None,CLIFE)]
        [TestCase(Strength.Weak, CLIFE)]
        [TestCase(Strength.Normal, CLIFE)]
        [TestCase(Strength.Strong, CLIFE)]
        public void DefenceCheck(Strength str,int expectedLife)
        {
            _warrior.Defend(1);
           // _warrior.GetAttacked(str);
            Assert.AreEqual(expectedLife, _warrior.CheckMe().Life);
        }

        [Test]
        [TestCase(0, CLIFE + 1)]
        [TestCase(1, CLIFE + 1)]
        [TestCase(3, CLIFE + 3)]
        public void TestRest(int time, int expectation)
        {
            _warrior.Rest(time);
            Assert.AreEqual(expectation, _warrior.CheckMe().Life);
        }

        [Test]
        [TestCase(State.Interrupted)]

        public void TestInterrupt(State expectedState)
        {
            _warrior.Interrupt();
            Console.WriteLine(_warrior.Check().State);
            Assert.AreEqual(expectedState,_warrior.CheckMe().State);
        }
    }
}
//TearDown (after every)
//FixtureTearDown (after all)