using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using log4net;
using RobotsAtWar.Client.Enums;
using RobotsAtWar.Client.Tools;
using RobotsAtWar.Enums;

namespace RobotsAtWar.Client
{
    public class WarriorClient
    {
        private const string ServerUrl = "ServerUrl";
        private const string WarriorName = "WarriorName";

        private static ILog _logger;

        public static bool Registered = false;

        public static WarriorState myInfo = new WarriorState();

        public WarriorClient()
        {
            _logger = LogManager.GetLogger(typeof(WarriorClient));
        }
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

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    retry = false;
                    Registered = true;
                }
                catch (Exception)
                {
                    ClearCurrentConsoleLine(1, 0);
                    Console.WriteLine("Connecting now...");
                    _logger.Info("Connecting now...");
                    Thread.Sleep(500);
                }
                _logger.Info("I have succesfully registered!");
            }

        }
        public void Register(string warriorName,string friendName)
        {
            bool retry = true;
            while (retry)
            {
                try
                {

                    // TODO: move http logic to a wrapper class
                    var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(ServerUrl) + "RegistrationWithFriend");
                    request.Timeout = 100000;

                    var data = Encoding.ASCII.GetBytes("=" + warriorName +","+ friendName);

                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    retry = false;
                    Registered = true;
                }
                catch (Exception)
                {
                    ClearCurrentConsoleLine(1, 0);
                    Console.WriteLine("Connecting now...");
                    _logger.Info("Connecting now...");
                    Thread.Sleep(500);
                }
                _logger.Info("I have succesfully registered!");
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
        
        public Response Attack(Strength strength)
        {
            int power = 0;
            string responseString = "";
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
            _logger.Info("Trying to deal "+power+" damage");
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
                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();


            }
            catch (Exception e)
            {
                _logger.Error(e);
                _logger.Info("Lost connection with server");
            }
            //Thread.Sleep(SleepTime(strength));
            Response resp = new Response();
            resp = StringToResponse(Int32.Parse(responseString));
            if (resp == Response.Dead)
            {
                WarriorBrain.enemyIsDead = true;
            }
            switch (resp)
            {
                case Response.Success:
                    _logger.Info("Success! I have dealt "+ power+" damage");
                    break;
                case Response.Interrupted:
                    _logger.Info("Fail... I got interrupted!");
                    break;
                case Response.Defending:
                    _logger.Info("Fail... Enemy was defending!");
                    break;
                case Response.Dead:
                    _logger.Info("Success! Enemy is dead!");
                    break;
            }
            return resp;
        }

        private Response StringToResponse(int value)
        {
            switch (value)
            {
                case 0:
                    return Response.Success;
                case 1:
                    return Response.Defending;
                case 2:
                    return Response.Interrupted;
                case 3:
                    return Response.Dead;
            }
            return Response.Success;
        }

        public void Defend(int time)
        {
                try
                {
                    _logger.Info("Defending for " + time + " seconds");
                    var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(ServerUrl) + "Defend");
                    request.Timeout = 100000;

                    var data = Encoding.ASCII.GetBytes("=" + time+ConfigSettings.ReadSetting(WarriorName));

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
                    //Thread.Sleep(time * 1000);
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                    _logger.Info("Lost connection with server");
                }
        }

        public void Rest(int time)
        {
            try
            {
                _logger.Info("Resting for " + time + " seconds");
                var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(ServerUrl) + "Rest");
                request.Timeout = 100000;

                var data = Encoding.ASCII.GetBytes("=" + time + ConfigSettings.ReadSetting(WarriorName));

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
            }
            catch (Exception e )
            {
                _logger.Error(e);
                _logger.Info("Lost connection with server");
            }
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
            catch (Exception e)
            {
                _logger.Error(e);
                _logger.Info("Lost connection with server");
            }
            //Thread.Sleep(500);
            //"{<State:0>},
            warriorState = ConvertResponseToWarriorState(responseString);
            _logger.Info("Enemy warrior state is " + warriorState.State + " life is " + warriorState.Life);
            return warriorState;
        }

        public void GetMyInfo()
        {
            string responseString = "";
            while (myInfo.Life > 0)
            {
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(ServerUrl) + "MyInfo");
                    request.Timeout = 100000;

                    var data = Encoding.ASCII.GetBytes("=" + ConfigSettings.ReadSetting(WarriorName));

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
                catch (Exception e)
                {
                    //_logger.Error(e);
                    _logger.Info("Lost connection with server");
                }

                myInfo = ConvertResponseToWarriorState(responseString);
               // Thread.Sleep(50);
            }

        }


        private WarriorState ConvertResponseToWarriorState(string value)
        {
            var warriorState = new WarriorState();
            string[] words = value.Split(',');
            words[0] = Regex.Match(words[0], @"\d+").Value;
            int life = Int32.Parse(Regex.Match(words[1], @"-?\d+").Value);

            warriorState.State = ConvertIntToState(Int32.Parse(words[0]));
            warriorState.Life = life;
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
