using System;
using QuantConnect.DesktopServer.TcpServer;
using QuantConnect.DesktopServer.WebServer;

namespace QuantConnect.DesktopServer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string tcpListenPort = "5558";
            if (args.Length == 1)
            {
                tcpListenPort = args[0];
            }

            Console.Write("Looking for saved backtests...");
            var backtestPersistanceManager = new BacktestPersistanceManager();
            var existingBacktests = backtestPersistanceManager.LoadStoredBacktests();
            Console.WriteLine("done");
            var sharedServerData = new SharedServerData(existingBacktests);
            var webServer = new NancyWebServer(sharedServerData);
            var tcpServer = new NetMQTcpServer();

            Console.Write("Starting webserver...");
            webServer.Start();
            Console.WriteLine("done");
            Console.Write("Starting tcpserver...");
            tcpServer.Start(tcpListenPort, new LeanMessageHandler(sharedServerData, backtestPersistanceManager));
            Console.WriteLine("done");

            Console.WriteLine("Press any keys to terminate server.");
            Console.ReadKey();
            Console.Write("Stopping tcpserver...");
            tcpServer.Stop();
            Console.WriteLine("done");
            Console.Write("Stopping webserver...");
            webServer.Stop();
            Console.WriteLine("done");
            tcpServer.Dispose();
            webServer.Dispose();
        }
    }
}
