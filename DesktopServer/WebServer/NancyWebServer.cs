using System;
using Nancy.Hosting.Self;

namespace QuantConnect.DesktopServer.WebServer
{
    public class NancyWebServer : IDisposable
    {
        private NancyHost _webServer;
        private ISharedServerData _sharedServerData;
        public NancyWebServer(ISharedServerData sharedServerData)
        {
            _sharedServerData = sharedServerData;
        }

        public void Start(string protocol = "http", string hostname = "localhost", string port = "8989" )
        {
            var listenUrl = new Uri(string.Format("{0}://{1}:{2}", protocol, hostname, port));
            _webServer = new NancyHost(new DesktopBootstrapper(_sharedServerData), new Uri[] { listenUrl });
            _webServer.Start();
            Console.WriteLine("Started Nancy");
        }

        public void Stop()
        {
            _webServer.Stop(); 
            Console.WriteLine("Stopped Nancy");
        }

        public void Dispose()
        {
            _webServer.Dispose();
        }
    }
}
