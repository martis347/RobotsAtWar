using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Business.Enums;
using log4net;
using Action = Business.Enums.Action;


namespace Business
{
    public class Battlefield
    {
        public static Warrior Warrior1;
        public static Warrior Warrior2;

        private static ILog _logger;

        public Battlefield()
        {
            _logger = LogManager.GetLogger(typeof(Battlefield));
        }

        
        public void Fight()
        {
            _logger = LogManager.GetLogger(typeof(Battlefield));

            StartFight();
        }

        private void StartFight()
        {
            while (Warrior1.IsAlive() && !stop)
            {
                Warrior1.ExecuteNextCommand();
            }
            if (!Warrior1.IsAlive())
            {
                Opponent.TellImDead();
            }
        }
    }
}
