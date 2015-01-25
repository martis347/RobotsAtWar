using System;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Business;

public class HttpApiService
{
    private readonly HttpSelfHostServer _server;
    private readonly HttpSelfHostConfiguration _config;
    private const string EventSource = "HttpApiService";

    public HttpApiService(Uri address)
    {
        if (!EventLog.SourceExists(EventSource))
        {
            EventLog.CreateEventSource(EventSource, "Application");
        }
        EventLog.WriteEntry(EventSource,
            String.Format("Creating server at {0}",
            address.ToString()));
        _config = new HttpSelfHostConfiguration(address);
        _config.Routes.MapHttpRoute("DefaultApi",
            "api/{controller}/{id}",
            new { id = RouteParameter.Optional }
        );
        _server = new HttpSelfHostServer(_config);
    }

    public void Start()
    {
        EventLog.WriteEntry(EventSource, "Opening HttpApiService server.");
        _server.OpenAsync();

        Opponent.WaitForOpponent();
        new Battlefield().Fight();
    }

    public void Stop()
    {
        _server.CloseAsync().Wait();
        _server.Dispose();
    }
}