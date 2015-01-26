using System;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Http.SelfHost;
using RobotsAtWar.Client;
using RobotsAtWar.Client.Tools;

namespace RobotsAtWar.Client.Host
{
    class HttpApiService
    {

        private readonly HttpSelfHostServer _server;
        private const string EventSource = "HttpApiService";

        public HttpApiService(Uri address)
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
                "{controller}/{war1}/{war2}",
                new { war1 = RouteParameter.Optional, war2 = RouteParameter.Optional }

            );
            _server = new HttpSelfHostServer(config);
        }

        public void Start()
        {
            //var me = new WarriorClient();
            WarriorClient me = new WarriorClient();

            EventLog.WriteEntry(EventSource, "Opening HttpApiService server.");
            _server.OpenAsync();

            string warriorsName = ConfigSettings.ReadSetting("WarriorName");
            WarriorBrain brain = new WarriorBrain(me);

            Console.WriteLine(warriorsName);
            brain.Start(warriorsName);

        }

        public void Stop()
        {
            _server.CloseAsync().Wait();
            _server.Dispose();
        }
    }
}
