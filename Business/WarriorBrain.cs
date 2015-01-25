using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Business;
using Business.Enums;
using Business.Tools;
using Action = Business.Enums.Action;

namespace Business
{
    class WarriorBrain
    {
        private WarriorClient _warriorClient;

        public WarriorBrain(WarriorClient warriorClient)
        {
            _warriorClient = warriorClient;
        }

        public void Start()
        {
            _warriorClient.Register(ConfigSettings.ReadSetting("WarriorName"));
            DateTime battleTime = DateTime.MinValue;
            while (battleTime == DateTime.MinValue)
            {
                battleTime = _warriorClient.GetBattleTime();
            }
            Thread.Sleep(battleTime - DateTime.UtcNow);
            Fight();
        }

        private void Fight()
        {
            int nextActionNumber = 0;
            while (true)
            {
                if (WarriorClient.Dead)
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
                    WarriorClient.Attack(command.Power);
                    break;
                case Action.Defend:
                    WarriorClient.Defend(command.Time);
                    break;
                case Action.Rest:
                    WarriorClient.Rest(command.Time);
                    break;
                case Action.Check:
                    WarriorClient.Check();
                    break;
                default:
                    WarriorClient.DoNothing();
                    break;
            }
        }
    }

    public class WarriorClient
    {
        private const string ServerUrl = "ServerUrl";

        public static bool Dead = false;
        public void Register(string warriorName)
        {
            while (true)
            {
                try
                {
                    // TODO: move http logic to a wrapper class
                    var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(ServerUrl) + "Registration");
                    request.Timeout = 100000;

                    var data = Encoding.ASCII.GetBytes(warriorName);

                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    // ReSharper disable once AssignNullToNotNullAttribute
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    Console.WriteLine("Response from server:" + responseString);


                }
                catch (Exception)
                {
                    Console.WriteLine("Connecting...");
                }
            }

        }

        //public DateTime GetBattleTime()
        //{

        //}

        public static void Attack(Strength strength)
        {
            int power = 0;
            switch (strength)
            {
                case Strength.Strong:
                    power = 4;
                    break;
                case Strength.Normal:
                    power = 2;
                    break;
                case Strength.Weak:
                    power = 1;
                    break;
            }
            var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(ServerUrl) + "Attack");
            request.Timeout = 100000;

            var data = Encoding.ASCII.GetBytes(power.ToString());

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            // ReSharper disable once AssignNullToNotNullAttribute
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Console.WriteLine("Response from server:" + responseString);
        }

        public static void Defend(int time)
        {
            
        }

        public static void Rest(int time)
        {
            
        }

        public static void Check()
        {
            
        }

        public static void DoNothing()
        {

        }

        public DateTime GetBattleTime()
        {
            return new DateTime();
        }
    }
}
