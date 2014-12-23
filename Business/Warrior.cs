
﻿using System;
﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using log4net;

namespace Business
{
    public enum State
    {
        Idle, Attacking, Defending, Resting, Checking, Interrupted
    }
    public enum Action
    {
        Attack = 1, Defend, Rest, Check
    }

    public struct Command
    {
        public Action Action;
        public int Time;
    }

    public struct Info
    {
        public State State;
        public int Life;
    }

    public class Warrior
    {

        private State _state;
        private int _life;
        private static ILog _logger;

        public Warrior(int life = 100)
        {
            _logger = LogManager.GetLogger(typeof (Warrior));
            _state = State.Idle;
        }

        public void Start()
        {
            _logger.Info("Service started.");
        }

        public void Stop()
        {
            _logger.Info("Service stoped.");

        }

        private void Interrupt()
        {
            if (_state != State.Resting && _state != State.Attacking) return;

            _state = State.Interrupted;
            _logger.Info("Warrior got interrupted!");
        }

        public int Attack(int time)
        {
            _logger.Info("Entering attack state");
            if (time > 3 || time < 1)
            {
                _logger.Info("You can't attack for that long!");
                return 0;
            }

            _state = State.Attacking;
            Thread.Sleep(time*1000);
            if (_state == State.Interrupted)
            {
                _logger.Info("Your attack has been interrupted!");
                return 0;
            }

            if (time == 3)
            {
                _logger.Info("You have dealt " + 4 + " damage!");
                _state = State.Idle;
                return time + 1;
            }
            _logger.Info("You have dealt " + time + " damage!");
            _state = State.Idle;
            return time;
        }

        public void GetAttacked(int damage)
        {
            if (damage < 1)
            {
                _logger.Info("Invalid damage!");
                return;
            }
            if (_state == State.Defending)
                _logger.Info("You have been attacked while defending! 0 Life points lost");
            else
            {
                _life -= damage;
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
            _state = State.Defending;
            Thread.Sleep(time*1000);
            _state = State.Idle;
        }

        public void Rest(int time)
        {
            if (time < 1) time = 1;

            _logger.Info(String.Format("Starting to rest for {0}s.", time));
            _state = State.Resting;
            Thread.Sleep(time*1000);
            if (_state == State.Resting)
            {
                _life += (int) Math.Pow(2, time - 1);
                _logger.Info("Successfully healed.");
            }
            else
            {
                _logger.Info("Healing was interrupted.");
            }
            _logger.Info("Resting comeplete.");
        }

        public Info Check()
        {
            _logger.Info("Checking current state and life.");
            Info info;
            info.Life = _life;
            info.State = _state;
            Thread.Sleep(200);
            return info;
        }

        public void SetCommand(Command command)
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
    }
}

