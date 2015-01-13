using System;
using System.IO;
using Business;
using log4net;
using log4net.Config;
using Topshelf;

namespace RobotsAtWar
{
    internal class Infrastructure
    {
        private static ILog _infrastructureLogger;

        private static void Main(string[] args)
        {
            _infrastructureLogger = LogManager.GetLogger(typeof(Infrastructure));
            XmlConfigurator.Configure(new FileInfo("..\\..\\App.config"));


            var restartDelay = (int)TimeSpan.FromMinutes(1).TotalMinutes;

            HostFactory.Run(config =>
            {
                _infrastructureLogger.Info("Attempting to start service");
                config.Service<HttpApiService>(svc =>
                {
                    svc.ConstructUsing(name => new HttpApiService(new Uri("http://localhost:1234")));
                    svc.WhenStarted(tc => tc.Start());
                    svc.WhenStopped(tc => tc.Stop());
                });


                config.SetServiceName("RobotService"); 
                config.SetDisplayName("RobotService");
                config.SetDescription("Topshelf Robot");
                config.RunAsLocalService();
                config.DependsOnEventLog();
                config.StartAutomatically();
                config.EnableServiceRecovery(recovery => recovery.RestartService(restartDelay));

            });
        }
    }
}
