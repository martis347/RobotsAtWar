using System;
using System.Threading;
using log4net;
using RobotsAtWar.Client.Tools;
using Action = RobotsAtWar.Enums.Action;

namespace RobotsAtWar.Client
{
   public class WarriorBrain
   {
       public static bool enemyIsDead = false;

       private static ILog _logger;

        private WarriorClient _warriorClient;

        public WarriorBrain(WarriorClient warriorClient)
        {
            _warriorClient = warriorClient;
            _logger = LogManager.GetLogger(typeof(WarriorBrain));
        }

        public void Start(string warriorName, string friendName)
        {
            if (friendName == "NotSet")
            {
                _warriorClient.Register(warriorName);                
            }
            else
                _warriorClient.Register(warriorName,friendName);
            Thread.Sleep(1000);
            //DateTime battleTime = DateTime.MinValue;
            //while (battleTime == DateTime.MinValue)
            //{
            //    battleTime = _warriorClient.GetBattleTime();
            //}
            //Thread.Sleep(battleTime - DateTime.UtcNow);
            Fight();
        }

        private void Fight()
        {
            int nextActionNumber = 0;
            while (!enemyIsDead)
            {
                if (WarriorClient.myInfo.Life <= 0)
                {
                    Console.WriteLine("I have lost the battle");
                    _logger.Info("I have lost the battle");
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                }
                ExecuteNextCommand(nextActionNumber++);
            }
            Console.Clear();
            _logger.Info("I have WON the battle!!!!!");
            Console.WriteLine("I have WON the battle!!!!!");
        }

        private void ExecuteNextCommand(int number)
        {
           
            Command command = SetNextCommand(number);
            ExecuteCommand(command);
        }

        private Command SetNextCommand(int number)
        {
            return Strategies.Testing[number % Strategies.Testing.Count];
        }

        private void ExecuteCommand(Command command)
        {
            switch (command.Action)
            {
                case Action.Attack:
                    _warriorClient.Attack(command.Power);
                    break;
                case Action.Defend:
                    _warriorClient.Defend(command.Time);
                    break;
                case Action.Rest:
                    _warriorClient.Rest(command.Time);
                    break;
                case Action.Check:
                    _warriorClient.Check();
                    break;
                default:
                    _warriorClient.DoNothing();
                    break;
            }
        }

       public void GetInfo()
       {
           
       }
    }

    
}
