using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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


                config.SetServiceName("Robot Service"); 
                config.SetDisplayName("That's a robot");
                config.SetDescription("Topshelf Robot");
                config.RunAsLocalService();
                config.DependsOnEventLog();
                config.StartAutomatically();
                config.EnableServiceRecovery(recovery => recovery.RestartService(restartDelay));

            });
        }
    }
}
