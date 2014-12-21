using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

    namespace Business
    {
        public class Warrior
        {
            private enum State
            {
                Idle , Attacking , Defending , Resting
            }

            private State _state;
            public int Life = 100;
            private static ILog _logger;

            public Warrior()
            {
                _logger = LogManager.GetLogger("RollingAppender");
                XmlConfigurator.Configure(new FileInfo("..\\..\\App.config"));
            }

            public void Start()
            {
                _logger.Info("Service started");
            }

            public void Stop()
            {

            }

            public int Attack(int time)
            {
                State = 1;
                switch (time)
                {
                    case 1:
                        _logger.Info("I Attacked with power 1");
                        Thread.Sleep(1000);
                        State = 0;
                        return 1;
                        break;
                    case 2:
                        _logger.Info("I Attacked with power 2");
                        Thread.Sleep(2000);
                        State = 0;
                        return 2;
                        break;
                    case 3:
                        _logger.Info("I Attacked with power 4");
                        Thread.Sleep(3000);
                        State = 0;
                        return 4;
                        break;
                    /* case default: ?
                         _logger.Info("Wrong attack time");
                         State = 0;
                         return 0;
                         break; */

                }
                return 0;
            }

            public void Defend(int time)
            {
                State = 2;
                _logger.Info("I am defending!");
                Thread.Sleep(time*1000);
            }
            public void Rest(int time)
            {
                State = 3;
                _logger.Info("I am resting!");
                for (int i = 1; i <= time; i++)
                {
                    Thread.Sleep(1000);
                    Life += i ^ 2;
                }
                State = 0;
            }
            public void Check()
            {
                State = 4;
                _logger.Info("I am checking!");
                Thread.Sleep(200);
                State = 0;
            }
        }
    }

