using System;
using System.Collections.Generic;
using System.Threading;
using Business;
using Business.Enums;
using log4net;

namespace RobotsAtWar.Server
{
    public class BattleField
    {

        private static Dictionary<string, Warrior> _warriorByName = new Dictionary<string, Warrior>(); 
        private DateTime _battleTime;
 
        public void RegisterWarrior(string warriorName)
        {
            _warriorByName.Add(warriorName, new Warrior(warriorName));

            if (_warriorByName.Count == 1)
            {
                _battleTime = DateTime.UtcNow.AddSeconds(5);
            }
        }

        public DateTime GetBattleTime()
        {
            return _battleTime;
        }

        public Warrior GetWarriorByName(string warriorName)
        {
            return _warriorByName[warriorName];
        }
        
        public static void WaitForWarriors()
        {
            Console.WriteLine("Waiting for players");
            while (_warriorByName.Count < 1)
            {
                ClearCurrentConsoleLine(1, 0);
                Console.WriteLine("Waiting for players");
                Thread.Sleep(2000);
            }
            ClearCurrentConsoleLine(2, 2);
            Console.WriteLine("Waiting other player to join");
            while (_warriorByName.Count < 2)
            {
                ClearCurrentConsoleLine(1, 0);
                Console.WriteLine("Waiting other player to join");
                Thread.Sleep(2000);
            }
            ClearCurrentConsoleLine(2, 2);
        }

        private static void ClearCurrentConsoleLine(int lineToClean, int lineToContinue)
        {
            Console.SetCursorPosition(0, Console.CursorTop - lineToClean);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor+lineToContinue);
        }


        public static void Start()
        {
            
        }
    }

    public class Warrior
    {
        private readonly string _warriorName;

        private static ILog _logger;

        public Warrior(string warriorName,ITimeMachine timeMachine, Opponent opponent, int life = 100, List<Command> strategy = null)
        {
            _logger = LogManager.GetLogger(typeof(Warrior));

            _warriorName = warriorName;
        }

        public static void Attack(Strength str)
        {

            Console.WriteLine(WarriorState.Life);
            WarriorState.State = State.Attacking;
            _logger.Info("I'm attacking");
            _timeMachine.Sleep(((int)str) * 1000, WarriorState, this);
            if (WarriorState.State == State.Interrupted)
            {
                _logger.Info("My attack has been interrupted!");
                return;
            }
            _opponent.GetAttacked(str);
        }

        public void GetAttacked(int str)
        {
            var damage = (int)str;
            Console.WriteLine(WarriorState.Life);

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

        public void Check()
        {
            _logger.Info("Checking current enemy state and life.");
            // _logger.Info("Enemy state is "+_enemy.WarriorState.State+" and Life is "+_enemy.WarriorState.Life);
            _timeMachine.Sleep(200, WarriorState, this);
            // return _enemy.WarriorState;
            //Console.WriteLine(_opponent.Check().Life + " " + _opponent.Check().State + " This is from get");
            Console.WriteLine(_opponent.Check());

        }
    }
}
