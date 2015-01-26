using System;
using System.Collections.Generic;
using System.Threading;


namespace RobotsAtWar.Server
{
    public class BattleField
    {

        private static Dictionary<string, Warrior> _warriorByName = new Dictionary<string, Warrior>();

        private TimeMachine _timeMachine;

        private DateTime _battleTime;

        public Warrior Warrior1 = new Warrior("Warrior1");
 
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

    
}
