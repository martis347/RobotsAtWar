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
            _warrior = new Warrior(100);
        }

        [Test]
        [TestCase(1,1)]
        [TestCase(2, 2)]
        [TestCase(3, 4)]
        [TestCase(4, 0)]
        [TestCase(-1, 0)]

        public void AttackCheck(int time, int power)
        {
            
            Assert.AreEqual(_warrior.Attack(time),power);
            
        }
        [Test]
        public void GetAttackedCheck()
        {
            _warrior.GetAttacked(1);
            _warrior.GetAttacked(2);
            _warrior.GetAttacked(3);
            _warrior.GetAttacked(4);
            _warrior.GetAttacked(-2);


        }
        [Test]
        public void DefenceCheck()
        {

            _warrior.Defend(1);
            _warrior.Defend(2);
            _warrior.Defend(-2);


        }
        [Test]
        [TestCase(0, 101)]
        [TestCase(1, 101)]
        [TestCase(3, 104)]
        public void TestRest(int time, int expectation)
        {
            _warrior.Rest(time);
            Assert.AreEqual(expectation, _warrior.Check().Life);
        }
        
    }
}
