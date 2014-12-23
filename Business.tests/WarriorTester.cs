using System;
using System.IO;
using log4net;
using log4net.Config;
using NUnit.Framework;

namespace Business.Tests
{
    [TestFixture]
    public class WarriorTester
    {
        private Warrior _warrior;
        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            XmlConfigurator.Configure();
        }
        [SetUp]
        //TearDown (after every)
        //FixtureTearDown (after all)
        public void SetUp()
        {
            _warrior = new Warrior();
        }

        [Test]
        public void AttackCheck()
        {

            var power = _warrior.Attack(1);
            Assert.AreEqual(1, power);
            var power2 = _warrior.Attack(2);
            Assert.AreEqual(2, power2);
            var power3 = _warrior.Attack(3);
            Assert.AreEqual(4, power3);
            var power4 = _warrior.Attack(345);
            Assert.AreEqual(0, power4);
            
        }
        [Test]
        public void GetAttackedCheck()
        {
            _warrior.GetAttacked(1);
            _warrior.GetAttacked(2);
            _warrior.GetAttacked(3);
            _warrior.GetAttacked(4);

        }
        [Test]
        public void DefenceCheck()
        {

            _warrior.Defend(1);
            _warrior.Defend(2);

        }
    }
}
