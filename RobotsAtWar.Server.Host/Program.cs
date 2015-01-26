using System;
using System.IO;
using log4net;
using log4net.Config;
using RobotsAtWar.Server.Host.Tools;
using Topshelf;

namespace RobotsAtWar.Server.Host
{
    class Program
    {
        private static ILog _logger;

        static void Main()
        {
            _logger = LogManager.GetLogger(typeof(Program));

            XmlConfigurator.Configure(new FileInfo("..\\..\\App.config"));

            HostFactory.Run(x =>
            {
                x.Service<BattleEngine>(s =>
                {
                    s.ConstructUsing(name => new BattleEngine(new Uri(ConfigSettings.ReadSetting("ServerUrl"))));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.StartManually();
                x.SetDescription("RobotsAtWar");
                x.SetDisplayName("RobotsAtWar");
                x.SetServiceName("RobotsAtWar");
            });
        }
    }
}
