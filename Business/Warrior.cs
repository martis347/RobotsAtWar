<<<<<<< HEAD
﻿using System.Threading;
=======
﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
>>>>>>> addad6b17b4522e28894710ca5bd7c292bb6ccea
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
<<<<<<< HEAD
            private int life;
            
=======
            private int Life;
            private Queue<Command> 
>>>>>>> addad6b17b4522e28894710ca5bd7c292bb6ccea
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
                if (time > 3)
                    return 0;

                _state = State.Attacking;
                Thread.Sleep(time*1000);
                if (time == 3)
                {
                    _state = State.Idle;
                    return time + 1;
                }
                _state = State.Idle;
                return time;
            }

            public void Attacking(int damage)
            {
                //Opponent.GetAttacked(damage);
                //Something like Opponent.GetAttacked(Attack(3));
            }


            public void GetAttacked(int damage)
            {
<<<<<<< HEAD
                life -= damage;
=======
>>>>>>> addad6b17b4522e28894710ca5bd7c292bb6ccea
            }


            public void Defend(int time)
            {
                _state = State.Defending;
                Thread.Sleep(time*1000);
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

