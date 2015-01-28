using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RobotsAtWar.Server.Enums;


namespace RobotsAtWar.Server
{
    public class BattleField
    {
       
        private int _numberOfWarriors = 1;
        private int _numberOfWarriorsWithFriend = 1;

        public static Dictionary<string, Warrior> _warriorByName = new Dictionary<string, Warrior>();
        public static Dictionary<string, Warrior> _warriorByNameWithFriend = new Dictionary<string, Warrior>();


        private readonly TimeMachine _timeMachine = new TimeMachine();

        private DateTime _battleTime;

        public void RegisterWarrior(string warriorName)
        {
            _warriorByName[warriorName] = new Warrior(warriorName,_timeMachine,10);
           // _warriorByName.Add(warriorName, new Warrior(warriorName,_timeMachine));

            if (_warriorByName.Count == 1)
            {
                _battleTime = DateTime.UtcNow.AddSeconds(5);
            }
        }

        public void RegisterWarriorWithFriend(string warriorName, string friendName)
        {
            _warriorByName[warriorName] = new Warrior(warriorName, _timeMachine, 10);
            _warriorByName[warriorName].SetOpponent(friendName);
        }

        public DateTime GetBattleTime()
        {
            return _battleTime;
        }

        public Warrior GetWarriorByName(string warriorName)
        {
            return _warriorByName[warriorName];
        }

        public Warrior GetWarriorWithFriendByName(string warriorName)
        {
            return _warriorByNameWithFriend[warriorName];
        }
        
        public void WaitForWarriors()
        {
            Console.WriteLine("Waiting for first player");
            while (_warriorByName.Count %2 == 0)
            {
                ClearCurrentConsoleLine(1, 0);
                Console.WriteLine("Waiting for first player");
                Thread.Sleep(2000);
            }
            ClearCurrentConsoleLine(2, 2);
            Console.WriteLine("Waiting other player to join");
            while (_warriorByName.Count %2 == 1)
            {
                ClearCurrentConsoleLine(1, 0);
                Console.WriteLine("Waiting other player to join");
                Thread.Sleep(500);
            }
            ClearCurrentConsoleLine(2, 2);
        }

        public void WaitForWarriorsWithFriend()
        {
            Console.WriteLine("Waiting for first player");
            while (_warriorByNameWithFriend.Count % 2 == 0)
            {
                ClearCurrentConsoleLine(1, 0);
                Console.WriteLine("Waiting for first player");
                Thread.Sleep(2000);
            }
            ClearCurrentConsoleLine(2, 2);
            Console.WriteLine("Waiting other player to join");
            while (_warriorByNameWithFriend.Count % 2 == 1)
            {
                ClearCurrentConsoleLine(1, 0);
                Console.WriteLine("Waiting other player to join");
                Thread.Sleep(500);
            }
            ClearCurrentConsoleLine(2, 2);
        }

        private void ClearCurrentConsoleLine(int lineToClean, int lineToContinue)
        {
            Console.SetCursorPosition(0, Console.CursorTop - lineToClean);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor+lineToContinue);
        }

        public void Start()
        {
            _warriorByName.ElementAt(_numberOfWarriors).Value.SetOpponent(_warriorByName.ElementAt(_numberOfWarriors+1).Key);
            _warriorByName.ElementAt(++_numberOfWarriors).Value.SetOpponent(_warriorByName.ElementAt(_numberOfWarriors-1).Key);
            _numberOfWarriors++;


        }

        
    }

    
}
