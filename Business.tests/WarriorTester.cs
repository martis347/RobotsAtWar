using System;
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

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            XmlConfigurator.Configure();
        }

        [SetUp]
        public void SetUp()
        {
            _warrior = new Warrior(new FakeTimeMachine(), 100);
        }

        [Test]
        [TestCase(Strength.None, State.Attacking, State.Attacking)]
        [TestCase(Strength.Weak, State.Attacking, State.Attacking)]
        [TestCase(Strength.Normal, State.Attacking, State.Attacking)]
        [TestCase(Strength.Strong, State.Attacking, State.Attacking)]
        public void AttackCheck(Strength str, State state,State expected)
        {
            _warrior.Attack(str);
            Assert.AreEqual(expected,_warrior.Check().State);
        }

        [Test]
        [TestCase(Strength.None, CLIFE)]
        [TestCase(Strength.Weak, CLIFE-1)]
        [TestCase(Strength.Normal, CLIFE-2)]
        [TestCase(Strength.Strong, CLIFE-4)]
        public void GetAttackedCheck(Strength str,int expectedLife)
        {
            _warrior.GetAttacked(str);
            Assert.AreEqual(expectedLife,_warrior.Check().Life);

        }

        [Test]
        [TestCase(Strength.None,CLIFE)]
        [TestCase(Strength.Weak, CLIFE)]
        [TestCase(Strength.Normal, CLIFE)]
        [TestCase(Strength.Strong, CLIFE)]
        public void DefenceCheck(Strength str,int expectedLife)
        {
            _warrior.Defend(1);
            _warrior.GetAttacked(str);
            Assert.AreEqual(expectedLife, _warrior.Check().Life);
        }

        [Test]
        [TestCase(0, CLIFE+1)]
        [TestCase(1, CLIFE + 1)]
        [TestCase(3, CLIFE + 4)]
        public void TestRest(int time, int expectation)
        {
            _warrior.Rest(time);
            Assert.AreEqual(expectation, _warrior.Check().Life);
        }
    }
}
//TearDown (after every)
//FixtureTearDown (after all)