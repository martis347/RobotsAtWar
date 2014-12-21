using log4net;

namespace Business
    {
        public class Warrior
        {
            private enum State
            {
                Idle , Attacking , Defending , Resting
            }

            private State _state;
            public int Life;
            private static ILog _logger;

            public Warrior(int life = 100)
            {
                _logger = LogManager.GetLogger(typeof(Warrior));
                _state = State.Idle;
            }

            public void Start()
            {
                _logger.Info("Service started");
            }

            public void Stop()
            {

            }

            public int Attack()
            {
                
                return 0;
            }

            public void Defend(int time)
            {

            }
            public void Rest(int time)
            {

            }
            public void Check()
            {

            }
        }
    }

