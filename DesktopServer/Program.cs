using System;
using QuantConnect.DesktopServer.TcpServer;
using QuantConnect.DesktopServer.WebServer;

namespace QuantConnect.DesktopServer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string port = "5558";
            if (args.Length == 1)
            {
                port = args[0];
            }

            var tcpServer = new NetMQTcpServer();
            var backtestPersistanceManager = new BacktestPersistanceManager();
            var existingBacktests = backtestPersistanceManager.LoadStoredBacktests();
            var sharedServerData = new SharedServerData(existingBacktests);
            var webServer = new NancyWebServer(sharedServerData);
            tcpServer.Start(port, new LeanMessageHandler(sharedServerData, backtestPersistanceManager));
            webServer.Start();

            Console.WriteLine("Press any keys to terminate server.");
            Console.ReadKey();
            tcpServer.Stop();
            webServer.Stop();
}
    }
}
