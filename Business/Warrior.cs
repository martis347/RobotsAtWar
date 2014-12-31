﻿using System;
﻿using Business.Enums;
﻿using log4net;
using Action = Business.Enums.Action;

namespace Business
{
    public class Warrior : IResetable
    {
        private readonly ITimeMachine _timeMachine;

        public WarriorState WarriorState { get; private set; }

        private static ILog _logger;


        public Warrior(ITimeMachine timeMachine, int life = 100)
        {
            if (timeMachine == null) throw new ArgumentNullException("timeMachine");
            _timeMachine = timeMachine;

            _logger = LogManager.GetLogger(typeof (Warrior));

            WarriorState = new WarriorState{Life = life};
        }


        public void Start()
        {
            _logger.Info("Service started.");
            var command = new Command {Action = Action.Attack, Time = 2};
            SetCommand(command);
            command.Action = Action.Check;
            SetCommand(command);
        }

        public void Stop()
        {
            _logger.Info("Service stoped.");
        }

        public int Attack(int time)
        {
            _logger.Info("Entering attack state");
            if (time > 3 || time < 1)
            {
                _logger.Info("You can't attack for that long!");
                return 0;
            }

            WarriorState.State = State.Attacking;
            _timeMachine.Sleep(time * 1000, this);
            if (WarriorState.State == State.Interrupted)
            {
                _logger.Info("Your attack has been interrupted!");
                return 0;
            }

            if (time == 3)
            {
                _logger.Info("You have dealt " + 4 + " damage!");
                WarriorState.State = State.Idle;
                return time + 1;
            }
            _logger.Info("You have dealt " + time + " damage!");
            WarriorState.State = State.Idle;
            return time;
        }

        public void GetAttacked(int damage)
        {
            if (damage < 1)
            {
                _logger.Info("Invalid damage!");
                return;
            }
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
            WarriorState.State = State.Idle;
        }

        public void Rest(int time)
        {
            if (time < 1) time = 1;

            _logger.Info(String.Format("Starting to rest for {0}s.", time));
            WarriorState.State = State.Resting;
            _timeMachine.Sleep(time * 1000, this);
            if (WarriorState.State == State.Resting)
            {
                WarriorState.Life += (int)Math.Pow(2, time - 1);
                _logger.Info("Successfully healed.");
            }
            else
            {
                _logger.Info("Healing was interrupted.");
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
                    Attack(command.Time);
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

