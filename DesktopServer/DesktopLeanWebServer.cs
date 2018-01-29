using System;
using Nancy.Hosting.Self;
using QuantConnect.DesktopServer.WebServer;

namespace QuantConnect.DesktopServer
{
    public class DesktopLeanWebServer : IDisposable
    {
        private NancyHost _webServer;
        private IDesktopServerData _desktopServerData;
        public DesktopLeanWebServer(IDesktopServerData desktopServerData)
        {
            _desktopServerData = desktopServerData;
        }

        public void Start()
        {
            var httpHostName = "localhost";
            var httpPort = "8989";
            var httpProtocol = "http";
            var listenUrl = new Uri(string.Format("{0}://{1}:{2}", httpProtocol, httpHostName, httpPort));
            _webServer = new NancyHost(new DesktopBootstrapper(_desktopServerData), new Uri[] { listenUrl });
            _webServer.Start();            
        }

        public void Stop()
        {
            _webServer.Stop(); 
        }

        public void Dispose()
        {
            _webServer.Dispose();
        }
    }
}
