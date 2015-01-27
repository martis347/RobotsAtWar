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

        public WarriorState WarriorState = new WarriorState();

        public string Opponent;

        public Warrior(string warriorName, ITimeMachine timeMachine, int life = 100)
        {
            _logger = LogManager.GetLogger(typeof(Warrior));

            _warriorName = warriorName;
            if (timeMachine == null) throw new ArgumentNullException("timeMachine");
            _timeMachine = timeMachine;

            WarriorState = new WarriorState { Life = life };

        }

        public bool Attack(string name, Strength str)
        {
            BattleFieldSingleton.BattleField.GetWarriorByName(name).WarriorState.State = State.Attacking;
            _logger.Info(_warriorName + " Is attacking");
            _timeMachine.Sleep(((int)str) * 1000, WarriorState, this);
            if (WarriorState.State == State.Interrupted)
            {
                _logger.Info(_warriorName + " attack was interrupted");
                return false;
            }
            return (BattleFieldSingleton.BattleField.GetWarriorByName(
                BattleFieldSingleton.BattleField.GetWarriorByName(name).Opponent).GetAttacked(str));

        }

        public bool GetAttacked(Strength str)
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
            {
                _logger.Info(_warriorName + " has been attacked while defending! 0 Life points lost");
                return false;
            }
            
             WarriorState.Life -= damage;
             _logger.Info(_warriorName+" has lost " + damage + " life points!");
             Interrupt();
            return true;
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

        public WarriorState GetChecked()
        {
            //_logger.Info("Checking current "+ _warriorName+" state and life.");
             _logger.Info(_warriorName+" State is "+WarriorState.State+" and Life is "+WarriorState.Life);
            _timeMachine.Sleep(200, WarriorState, this);
             return WarriorState;

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
