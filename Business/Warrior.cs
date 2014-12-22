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
            private int life;
            
            private static ILog _logger;

            public Warrior(int life = 100)
            {
                _logger = LogManager.GetLogger(typeof(Warrior));
                _state = State.Idle;
            }

            public void Start()
            {
                _logger.Info("Service started");
            }

            public void Stop()
            {

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
                life -= damage;
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

