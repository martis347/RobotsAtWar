using System;
using log4net;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server
{
    public class Warrior : IResetable
    {
        private readonly string _warriorName;

        private static ILog _logger;

        private readonly ITimeMachine _timeMachine;

        private WarriorState WarriorState { get; set; }

        public Warrior(string warriorName, ITimeMachine timeMachine, int life = 100)
        {
            _logger = LogManager.GetLogger(typeof(Warrior));

            _warriorName = warriorName;
            if (timeMachine == null) throw new ArgumentNullException("timeMachine");
            _timeMachine = timeMachine;

            _logger = LogManager.GetLogger(typeof(Warrior));

            WarriorState = new WarriorState { Life = life };

        }

        public Warrior(string warriorName)
        {
            _warriorName = warriorName;            
        }

        public void Attack(Strength str)
        {

            Console.WriteLine(WarriorState.Life);
            WarriorState.State = State.Attacking;
            _logger.Info("I'm attacking");
            _timeMachine.Sleep(((int)str) * 1000, WarriorState, this);
            if (WarriorState.State == State.Interrupted)
            {
                _logger.Info("My attack has been interrupted!");
                return;
            }
            //_opponent.GetAttacked(str);
        }

        public void GetAttacked(int str)
        {
            var damage = (int)str;
            Console.WriteLine(WarriorState.Life);

            if (WarriorState.State == State.Defending)
                _logger.Info("Enemy has been attacked while defending! 0 Life points lost");
            else
            {
                WarriorState.Life -= damage;
                _logger.Info("I have lost " + damage + " life points!");
                // Interrupt();
            }
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
    }
}
