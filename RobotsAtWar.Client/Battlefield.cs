using log4net;

namespace RobotsAtWar.Client
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
            while (Warrior1.IsAlive())
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
