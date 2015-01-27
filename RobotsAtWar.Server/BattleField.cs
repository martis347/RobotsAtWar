using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RobotsAtWar.Server.Enums;


namespace RobotsAtWar.Server
{
    public class BattleField
    {
       

        public static Dictionary<string, Warrior> _warriorByName = new Dictionary<string, Warrior>();

        private readonly TimeMachine _timeMachine = new TimeMachine();

        private DateTime _battleTime;

        public void RegisterWarrior(string warriorName)
        {
            _warriorByName[warriorName] = new Warrior(warriorName,_timeMachine);
           // _warriorByName.Add(warriorName, new Warrior(warriorName,_timeMachine));

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
        
        public void WaitForWarriors()
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
            _warriorByName.ElementAt(0).Value.SetOpponent(_warriorByName.ElementAt(1).Key);
            _warriorByName.ElementAt(1).Value.SetOpponent(_warriorByName.ElementAt(0).Key);



        }



    }

    
}
