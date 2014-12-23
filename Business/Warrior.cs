
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
            private Action action;
            private int time;
        }
        
        public class Warrior
        {
            private State _state;
            private int Life;
            //private Queue<Command> 
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
                if (time > 3)
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
                    _logger.Info("You have dealt "+4+" damage!");
                    _state = State.Idle;
                    return time + 1;
                }
                _logger.Info("You have dealt " + time + " damage!");
                _state = State.Idle;
                return time;
            }

            public void GetAttacked(int damage)
            {
                if (_state == State.Defending)
                    _logger.Info("You have been attacked while defending! 0 Life points lost");
                else
                {
                    Life -= damage;
                    _logger.Info("You have lost " + damage + " life points!");
                }
                    
            }


            public void Defend(int time)
            {
                _logger.Info("Entering defence state!");
                _state = State.Defending;
                Thread.Sleep(time*1000);
                _state = State.Idle;
            }

            public void Rest(int time)
            {
                 
            }

            public void Check()
            {

            }

            public void SetCommand(Command command)
            {
                
            }
        }
    }

