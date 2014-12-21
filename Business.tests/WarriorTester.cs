using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Business;
using log4net.Config;

namespace Business.tests
{
    [TestFixture]
    public class WarriorTester
    {
        private Warrior _tester;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            XmlConfigurator.Configure();
        }

        [Test]
        public void Test()
        {

            _tester.Check();
        }
    }
}
