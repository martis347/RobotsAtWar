﻿using System;
﻿using System.Collections.Generic;
﻿using Business.Enums;
﻿using log4net;
using Action = Business.Enums.Action;

namespace Business
{
    public class Warrior : IResetable
    {

        private Warrior _enemy;

        private readonly ITimeMachine _timeMachine;

        private WarriorState WarriorState { get;  set; }

        private static ILog _logger;

        private readonly List<Command> _strategy = new List<Command>();

        public Warrior(ITimeMachine timeMachine, int life = 100, List<Command> strategy = null)
        {
            if (timeMachine == null) throw new ArgumentNullException("timeMachine");
            _timeMachine = timeMachine;

            _logger = LogManager.GetLogger(typeof (Warrior));

            WarriorState = new WarriorState{Life = life};
            _strategy = _strategy == null ? new List<Command>() : strategy;
        }

        public void SetEnemy(Warrior enemyWarrior)
        {
            _enemy = enemyWarrior;
        }

        public void FightMe()
        {
            if (_strategy.Count == 0) return;
            int it = 0;
            while (WarriorState.State != State.Dead && _enemy.WarriorState.State != State.Dead )
            {
                SetCommand(_strategy[it++ % _strategy.Count]);
                if (WarriorState.Life <= 0 )
                {
                    WarriorState.State = State.Dead;
                }
                if (_enemy.WarriorState.Life <= 0)
                {
                    IWon();
                }
            }
        }

        public void Attack(Strength str)
        {
            WarriorState.State = State.Attacking;
            _logger.Info("I'm attacking");
            _timeMachine.Sleep(((int)str) * 1000 );
            if (WarriorState.State == State.Interrupted)
            {
                _logger.Info("My attack has been interrupted!");
                return;
            }
            _enemy.GetAttacked(str);
            _timeMachine.Reset(this);
            //Pataisyt reset
        }

        public void GetAttacked(Strength str)
        {
            var damage = (int) str;
            if (str == Strength.Strong)
                damage = 4;

            if (WarriorState.State == State.Defending)
                _logger.Info("Enemy has been attacked while defending! 0 Life points lost");
            else
            {
                WarriorState.Life -= damage;
                _logger.Info("Enemy has lost " + damage + " life points!");
                Interrupt();
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
            _timeMachine.Sleep(time * 1000 );
            _timeMachine.Reset(this);
        }

        public void Rest(int time)
        {
            if (time < 1) time = 1;

            _logger.Info(String.Format("I'm starting to rest for {0}s.", time));
            WarriorState.State = State.Resting;
            _timeMachine.Sleep(time * 1000 );
            if (WarriorState.State == State.Interrupted)
            {
                _logger.Info("My healing was interrupted.");
            }
            else
            {
                WarriorState.Life += time;
                _logger.Info("I healed successfully!");
            }
            _timeMachine.Reset(this);
        }

        public WarriorState Check()
        {
            _logger.Info("Checking current enemy state and life.");
            _logger.Info("Enemy state is "+_enemy.WarriorState.State+" and Life is "+_enemy.WarriorState.Life);
           // _timeMachine.Sleep(200, this);
            _timeMachine.Reset(this);
            return _enemy.WarriorState;
        }

        public WarriorState CheckMe()
        {
            _logger.Info("Checking current enemy state and life.");
            _logger.Info("Enemy state is " + WarriorState.State + " and Life is " + WarriorState.Life);
            _timeMachine.Reset(this);
            return WarriorState;
        }

        private void IWon()
        {
            _logger.Info("I have won the battle!");
        }

        public void Interrupt()
        {
            WarriorState.State = State.Interrupted;
            _logger.Info("Enemy got interrupted!");
        }

        private void SetCommand(Command command)
        {
            switch (command.Action)
            {
                case Action.Attack:
                    Attack(command.Power);
                    break;
                case Action.Defend:
                    Defend(command.Time);
                    break;
                case Action.Rest:
                    Rest(command.Time);
                    break;
                case Action.Check:
                    Check();
                    break;
            }
        }

        public void Reset()
        {
            WarriorState.State = State.Idle;
        }
    }


}