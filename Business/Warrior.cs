using System.Collections;
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
            private Queue<Command> 
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
                _state = State.Interrupted;
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

            }

            public void Check()
            {

            }

            public void SetCommand(Command command)
            {
                
            }
        }
    }

