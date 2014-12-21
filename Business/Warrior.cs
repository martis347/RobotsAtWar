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
            private Action action;
            private int time;
        }
        
        public class Warrior
        {
            

            private State _state;
            private int Life;
            private Queue<Command> commandQueue;
            private static ILog _logger;

            public Warrior(int life = 100)
            {
                _logger = LogManager.GetLogger(typeof(Warrior));
                _state = State.Idle;
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
                _logger.Info(String.Format("Starting to rest for {0}s.", time));
                _state = State.Resting;
                Thread.Sleep(time*1000);
                if (_state == State.Resting)
                {
                    Life += (int)Math.Pow(2, time);
                    _logger.Info("Successfully healed.");
                }
                else
                {
                    _logger.Info("Healing was interrupted.");
                }
                _logger.Info("Resting comeplete.");
            }

            public void Check()
            {

            }

            public void SetCommand(Command command)
            {
                
            }
        }
    }

