using log4net;
using log4net.Config;
using NUnit.Framework;

namespace Business.tests
{
    [TestFixture]
    public class WarriorTester
    {
        private Warrior _warrior;
        private readonly ILog testLogger;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            XmlConfigurator.Configure();
        }

        [Test]
        public void TestCheck()
        {

            _warrior.Check();
        }
    }
}
