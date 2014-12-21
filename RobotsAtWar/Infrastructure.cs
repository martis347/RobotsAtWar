using System;
using System.IO;
using Business;
using log4net;
using log4net.Config;
using Topshelf;

namespace RobotsAtWar
{
    class Infrastructure
    {
        private static ILog _infrastructureLogger;

        static void Main(string[] args)
        {
            _infrastructureLogger = LogManager.GetLogger(typeof(Infrastructure));
            XmlConfigurator.Configure(new FileInfo("..\\..\\App.config"));


            var restartDelay = (int)TimeSpan.FromMinutes(1).TotalMinutes;

            HostFactory.Run(config =>
            {

                _infrastructureLogger.Info("Attempting to start service");
                config.Service<Warrior>(svc =>
                {
                    svc.ConstructUsing(s => new Warrior());
                    svc.WhenStarted(s => s.Start());
                    svc.WhenStopped(s => s.Stop());
                });


                config.SetServiceName("RobotService"); 
<<<<<<< HEAD
                config.SetDisplayName("Thaobot");
                config.SetDescription("TopshelfRobot");
=======
                config.SetDisplayName("RobotService");
                config.SetDescription("Topshelf Robot");
>>>>>>> 2219dd58d94b558eff34a19f939cb727fb3dca4e
                config.RunAsLocalService();
                config.DependsOnEventLog();
                config.StartAutomatically();
                config.EnableServiceRecovery(recovery => recovery.RestartService(restartDelay));

            });
        }
    }
}
