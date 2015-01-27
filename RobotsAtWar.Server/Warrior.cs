using System;
using log4net;
using log4net.Core;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server
{
    public class Warrior : IResetable
    {
        private readonly string _warriorName;

        private static ILog _logger;

        private readonly ITimeMachine _timeMachine = new TimeMachine();

        private WarriorState WarriorState = new WarriorState();

        public string Opponent;

        public Warrior(string warriorName, ITimeMachine timeMachine, int life = 100)
        {
            _logger = LogManager.GetLogger(typeof(Warrior));

            _warriorName = warriorName;
            if (timeMachine == null) throw new ArgumentNullException("timeMachine");
            _timeMachine = timeMachine;

            WarriorState = new WarriorState { Life = life };

        }

        public Warrior(string warriorName)
        {
            _warriorName = warriorName;
            _logger = LogManager.GetLogger(typeof(Warrior));
        
        }

        public void GetAttacked(Strength str)
        {
            int damage = 0;

            switch (str)
            {
                case Strength.Strong:
                    damage = 3;
                    break;
                case Strength.Normal:
                    damage = 2;
                    break;
                case Strength.Weak:
                    damage = 1;
                    break;
            }

            if (WarriorState.State == State.Defending)
                _logger.Info(_warriorName+" has been attacked while defending! 0 Life points lost");
            else
            {
                WarriorState.Life -= damage;
                _logger.Info(_warriorName+" has lost " + damage + " life points!");
                Interrupt();
            }
        }

        private void Interrupt()
        {
            WarriorState.State = State.Interrupted;
        }

        public void Defend(int time)
        {
            if (time < 1)
            {
                _logger.Info("Invalid time!");
                return;
            }
            _logger.Info("I'm entering defence state!");
            WarriorState.State = State.Defending;
            _timeMachine.Sleep(time * 1000, WarriorState, this);
        }

        public void Rest(int time)
        {
            if (time < 1) time = 1;

            _logger.Info(String.Format("I'm starting to rest for {0}s.", time));
            WarriorState.State = State.Resting;
            _timeMachine.Sleep(time * 1000, WarriorState, this);
            if (WarriorState.State == State.Interrupted)
            {
                _logger.Info("My healing was interrupted.");
            }
            else
            {
                WarriorState.Life += time;
                _logger.Info("I healed successfully!");
            }
        }

        public void Check()
        {
            _logger.Info("Checking current enemy state and life.");
            // _logger.Info("Enemy state is "+_enemy.WarriorState.State+" and Life is "+_enemy.WarriorState.Life);
            _timeMachine.Sleep(200, WarriorState, this);
            // return _enemy.WarriorState;
            //Console.WriteLine(_opponent.Check().Life + " " + _opponent.Check().State + " This is from get");
            //  Console.WriteLine(_opponent.Check());

        }

        public void Reset()
        {
            WarriorState.State = State.Idle;
        }

        public void SetOpponent(string opponent)
        {
            Opponent = opponent;
        }

        
    }
}
