using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using RobotsAtWar.Client.Tools;
using RobotsAtWar.Enums;

namespace RobotsAtWar.Client
{
    public class WarriorClient
    {
        private const string ServerUrl = "ServerUrl";

        public bool Dead = false;

        public void Register(string warriorName)
        {
            bool retry = true;
            while (retry)
            {
                try
                {

                    // TODO: move http logic to a wrapper class
                    var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(ServerUrl) + "Registration");
                    request.Timeout = 100000;

                    var data = Encoding.ASCII.GetBytes("="+warriorName);

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
                    retry = false;
                }
                catch (Exception)
                {
                    ClearCurrentConsoleLine(1, 0);
                    Console.WriteLine("Connecting now...");
                   // Thread.Sleep(500);
                    

                    
                }
            }

        }

        private static void ClearCurrentConsoleLine(int lineToClean, int lineToContinue)
        {
            Console.SetCursorPosition(0, Console.CursorTop - lineToClean);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor + lineToContinue);
        }
        //public DateTime GetBattleTime()
        //{

        //}
        
        public void Attack(Strength strength)
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

            var data = Encoding.ASCII.GetBytes("="+power);

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

        public void Defend(int time)
        {

        }

        public void Rest(int time)
        {

        }

        public void Check()
        {

        }

        public void DoNothing()
        {

        }

        public DateTime GetBattleTime()
        {
            return new DateTime();
        }
    }
}
