using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using log4net;
using Microsoft.Win32;

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
                _logger = LogManager.GetLogger(typeof(Warrior));
                _state = State.Idle;
                _life = life;
            }

            public void Start()
            {
                _logger.Info("Service started.");
            }

            public void Stop()
            {
                
            }

            private void Interrupt()
            {
                 if (_state == State.Resting || _state == State.Attacking)
                 {
                     _state = State.Interrupted;
                 }
            }

            public int Attack(int time)
            {
                
                return 0;
            }


            public void GetAttacked(int damage)
            {
            }


            public void Defend(int time)
            {

            }

            public void Rest(int time)
            {
                if (time < 1) time = 1;

                _logger.Info(String.Format("Starting to rest for {0}s.", time));
                _state = State.Resting;
                Thread.Sleep(time*1000);
                if (_state == State.Resting)
                {
                    _life += (int)Math.Pow(2, time-1);
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

