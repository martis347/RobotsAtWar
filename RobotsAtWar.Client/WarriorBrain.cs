using System;
using System.Threading;
using RobotsAtWar.Client.Tools;
using Action = RobotsAtWar.Enums.Action;

namespace RobotsAtWar.Client
{
   public class WarriorBrain
    {
        private WarriorClient _warriorClient;

        public WarriorBrain(WarriorClient warriorClient)
        {
            _warriorClient = warriorClient;
        }

        public void Start(string warriorName)
        {
            _warriorClient.Register(warriorName);
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
            while (true)
            {
                if (_warriorClient.Dead)
                {
                    Environment.Exit(0);
                }
                ExecuteNextCommand(nextActionNumber++);
            }
        }

        private void ExecuteNextCommand(int number)
        {
           
            Command command = SetNextCommand(number);
            ExecuteCommand(command);
            Thread.Sleep(2000);
        }

        private Command SetNextCommand(int number)
        {
            return Strategies.Aggressive[number % Strategies.Aggressive.Count];
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
    }

    
}
