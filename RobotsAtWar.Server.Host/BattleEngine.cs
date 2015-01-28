using System;
using System.Diagnostics;
using System.Threading;
using System.Web.Http;
using System.Web.Http.SelfHost;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Host
{
    class BattleEngine
    {
        private readonly HttpSelfHostServer _server;
        private const string EventSource = "HttpApiService";

        public BattleEngine(Uri address)
        {
            if (!EventLog.SourceExists(EventSource))
            {
                EventLog.CreateEventSource(EventSource, "Application");
            }
            EventLog.WriteEntry(EventSource,
                String.Format("Creating server at {0}",
                address));
            var config = new HttpSelfHostConfiguration(address);
            config.Routes.MapHttpRoute("DefaultApi",
                "{controller}/{name}",
                new { name = RouteParameter.Optional}
                

            );
            _server = new HttpSelfHostServer(config);
        }
        BattleField battleField = new BattleField();

        public void Start()
        {
            EventLog.WriteEntry(EventSource, "Opening HttpApiService server.");
            _server.OpenAsync();

            Thread registrationThread = new Thread(Registration);
            registrationThread.Start();
            Thread registrationWithFriendThread = new Thread(RegistrationWithFriend);
            registrationWithFriendThread.Start();
            


        }

        private void RegistrationWithFriend()
        {
            battleField.WaitForWarriorsWithFriend();
            Console.WriteLine("Both friends connected");
        }

        private void Registration()
        {
            //while (true)
            //{
                battleField.WaitForWarriors();
                Console.WriteLine("Both connected");
           // }
            battleField.Start();
        }

        public void Stop()
        {
            _server.CloseAsync().Wait();
            _server.Dispose();
        }


    }
}
