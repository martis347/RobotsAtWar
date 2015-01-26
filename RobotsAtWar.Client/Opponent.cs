using System;
using System.IO;
using System.Net;
using System.Text;
using RobotsAtWar.Enums;
using RobotsAtWar.Client.Tools;

namespace RobotsAtWar.Client
{
    public class Opponent
    {
        private const string OPPONENT_URL = "OpponentUrl";

        public static void WaitForOpponent()
        {
            while (true)
            {
                try
                {
                    // TODO: move htto logilc to a wrapper class
                    var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(OPPONENT_URL) + "attack");
                    request.GetResponse();

                    Console.WriteLine("Found!!!");

                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Waiting...");
                }
            }
        }

        public static void Post(string action, string postData)
        {
            var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(OPPONENT_URL) + action);
            request.Timeout = 100000;

            var data = Encoding.ASCII.GetBytes(postData);


            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return;

        }

        public static void TellImDead()
        {
            var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(OPPONENT_URL) + "Death");

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
        public static string Get()
        {

            var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(OPPONENT_URL) + "Check");

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Console.WriteLine(responseString + "This is RESPONSE STRING!");

            return responseString;
        }
        public int Check()
        {
            int response = Int32.Parse(Get());
            
            //return Get();
            return response;
        }

        public string GetName()
        {
            return "set my name!";
        }

        public bool IsAlive()
        {
            return true;
        }

        public string GetState()
        {
            return "OMFG";
        }

        public void GetAttacked(Strength str)
        {
            int damage = 0;
            switch (str)
            {
                case Strength.None:
                    damage = 0;
                    break;
                case Strength.Weak:
                    damage = 1;
                    break;
                case Strength.Normal:
                    damage = 2;
                    break;
                case Strength.Strong:
                    damage = 4;
                    break;
            }
            Post("attack", "=" + damage);
        }
    }
}
