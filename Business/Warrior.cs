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

        private WarriorState WarriorState { get; set; }

        private static ILog _logger;

        private readonly List<Command> _strategy = new List<Command>();



        public Warrior(ITimeMachine timeMachine, int life = 100, List<Command> strategy = null)
        {
            if (timeMachine == null) throw new ArgumentNullException("timeMachine");
            _timeMachine = timeMachine;

            _logger = LogManager.GetLogger(typeof (Warrior));

            WarriorState = new WarriorState{Life = life};
           // _strategy = strategy;
            _strategy = _strategy == null ? new List<Command>() : strategy;
        }


        public void Start()
        {
            _logger.Info("Service started.");
            

        }

        public void SetEnemy(Warrior enemyWarrior)
        {
            _enemy = enemyWarrior;
        }
        public void FightMe()
        {
           // if (_strategy.Count == 0) return;
            int it = 0;
            Console.WriteLine(_strategy.Count);
            while (WarriorState.State != State.Dead )
            {
                SetCommand(_strategy[it++ % _strategy.Count]);
              // Console.WriteLine(_strategy.Count);
            }
        }
        public void Stop()
        {
            _logger.Info("Service stoped.");
        }

        public void Attack(Strength str)
        {
            WarriorState.State = State.Attacking;
            _logger.Info("Attacking");
            _timeMachine.Sleep(((int)str) * 1000, this);

            if (WarriorState.State == State.Interrupted)
            {
                _logger.Info("Your attack has been interrupted!");
                return;
            }

            //Enemy.Interrupt();
            //Enemy.GetAttacked(str);
        }

        public void GetAttacked(Strength str)
        {
            var damage = (int) str;
            if (str == Strength.Strong)
                damage = 4;

            if (WarriorState.State == State.Defending)
                _logger.Info("You have been attacked while defending! 0 Life points lost");
            else
            {
                WarriorState.Life -= damage;
                _logger.Info("You have lost " + damage + " life points!");
            }

        }

        public void Defend(int time)
        {
            if (time < 1)
            {
                _logger.Info("Invalid time!");
                return;
            }
            _logger.Info("Entering defence state!");
            WarriorState.State = State.Defending;
            _timeMachine.Sleep(time * 1000, this);
        }

        public void Rest(int time)
        {
            if (time < 1) time = 1;

            _logger.Info(String.Format("Starting to rest for {0}s.", time));
            WarriorState.State = State.Resting;
            _timeMachine.Sleep(time * 1000, this);
            if (WarriorState.State == State.Interrupted)
            {
                _logger.Info("Healing was interrupted.");
            }
            else
            {
                WarriorState.Life += (int)Math.Pow(2, time - 1);
                _logger.Info("Successfully healed.");
            }
            _logger.Info("Resting complete.");
        }

        public WarriorState Check()
        {
            _logger.Info("Checking current state and life.");

            return WarriorState;
        }

        private void Interrupt()
        {
            if (WarriorState.State != State.Resting && WarriorState.State != State.Attacking)
                return;
            WarriorState.State = State.Interrupted;
            _logger.Info("Warrior got interrupted!");
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