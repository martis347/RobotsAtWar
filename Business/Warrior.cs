﻿using System;
﻿using System.Collections.Generic;
﻿using Business.Enums;
﻿using log4net;
using Action = Business.Enums.Action;

namespace Business
{
    public class Warrior : IResetable
    {
        private Opponent _opponent;
        private readonly ITimeMachine _timeMachine;

        private WarriorState WarriorState { get;  set; }

        private static ILog _logger;

        private readonly List<Command> _strategy = new List<Command>();

        public Warrior(ITimeMachine timeMachine, Opponent opponent, int life = 100, List<Command> strategy = null)
        {
            if (timeMachine == null) throw new ArgumentNullException("timeMachine");
            _timeMachine = timeMachine;

            _logger = LogManager.GetLogger(typeof (Warrior));

            WarriorState = new WarriorState{Life = life};
            _strategy = _strategy == null ? new List<Command>() : strategy;
            _opponent = opponent;
            
        }


        public void ExecuteNextCommand()
        {
            if (_strategy.Count == 0)
            {
                _logger.Warn("0 strategies");
                return;
            }
            int index = 0;
            ExecuteCommand(_strategy[index++ % _strategy.Count]);
            

        }

        public void Attack(Strength str)
        {
            WarriorState.State = State.Attacking;
            _logger.Info("I'm attacking");
            _timeMachine.Sleep(((int)str) * 1000 ,WarriorState,this);
            if (WarriorState.State == State.Interrupted)
            {
                _logger.Info("My attack has been interrupted!");
                return;
            }
            _opponent.GetAttacked(str);
        }

        public void GetAttacked(int str)
        {
            var damage = (int) str;
            
            if (WarriorState.State == State.Defending)
                _logger.Info("Enemy has been attacked while defending! 0 Life points lost");
            else
            {
                WarriorState.Life -= damage;
                _logger.Info("I have lost " + damage + " life points!");
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

        public WarriorState Check()
        {
            _logger.Info("Checking current enemy state and life.");
           // _logger.Info("Enemy state is "+_enemy.WarriorState.State+" and Life is "+_enemy.WarriorState.Life);
            _timeMachine.Sleep(200, WarriorState, this);
           // return _enemy.WarriorState;
            return WarriorState;
        }

        public WarriorState CheckMe()
        {
            _logger.Info("Checking current enemy state and life.");
            _logger.Info("Enemy state is " + WarriorState.State + " and Life is " + WarriorState.Life);
            return WarriorState;
        }

        public bool IsAlive()
        {
            return (WarriorState.Life > 0);
        }

        public void Interrupt()
        {
            WarriorState.State = State.Interrupted;
            _logger.Info("Enemy got interrupted!");
        }

        private void ExecuteCommand(Command command)
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