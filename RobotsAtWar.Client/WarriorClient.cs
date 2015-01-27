﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using RobotsAtWar.Client.Tools;
using RobotsAtWar.Enums;

namespace RobotsAtWar.Client
{
    public class WarriorClient
    {
        private const string ServerUrl = "ServerUrl";
        private const string WarriorName = "WarriorName";

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
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(ServerUrl) + "Attack");
                request.Timeout = 100000;

                var data = Encoding.ASCII.GetBytes("=" + power + ConfigSettings.ReadSetting(WarriorName));

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
                Console.WriteLine("Lost connection with server");
            }
            
        }

        public void Defend(int time)
        {

        }

        public void Rest(int time)
        {

        }

        public WarriorState Check()
        {
            WarriorState warriorState = new WarriorState();
            string responseString = "";
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(ServerUrl) + "Check");
                request.Timeout = 100000;

                var data = Encoding.ASCII.GetBytes("=" +  ConfigSettings.ReadSetting(WarriorName));

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                // ReSharper disable once AssignNullToNotNullAttribute
                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                
                

            }
            catch (Exception)
            {
                Console.WriteLine("Lost connection with server");
            }
            
            warriorState = ConvertResponseToWarriorState(responseString);
            Console.WriteLine("Enemy warrior state is "+warriorState.State + " life is " + warriorState.Life);
            return warriorState;
        }

        private WarriorState ConvertResponseToWarriorState(string value)
        {
            var warriorState = new WarriorState();
            string[] words = value.Split(',');
            words[0] = Regex.Match(words[0], @"\d+").Value;
            words[1] = Regex.Match(words[1], @"\d+").Value;
            warriorState.State = ConvertIntToState(Int32.Parse(words[0]));
            warriorState.Life = Int32.Parse(words[1]);
            return warriorState;
        }

        private State ConvertIntToState(int valueFromResponse)
        {
            switch (valueFromResponse)
            {
                case 0:
                    return State.Idle;
                case 1:
                    return State.Attacking;
                case 2:
                    return State.Defending;
                case 3:
                    return State.Resting;
                case 4:
                    return State.Checking;
                case 5:
                    return State.Interrupted;
                case 6:
                    return State.Dead;
            }
            return State.Idle;
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
