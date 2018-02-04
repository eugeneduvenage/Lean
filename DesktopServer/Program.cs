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
            string httpListenPort = "8989";

            if (args.Length == 2)
            {
                tcpListenPort = args[0];
                httpListenPort = args[1];
            }

            Console.Write("Looking for saved backtests...");
            var backtestPersistanceManager = new BacktestPersistenceManager();
            var existingBacktests = backtestPersistanceManager.LoadStoredBacktests();
            Console.WriteLine("done");
            var sharedServerData = new SharedServerData(existingBacktests);
            var webServer = new NancyWebServer(sharedServerData);
            var tcpServer = new NetMQTcpServer();

            Console.Write("Starting webserver...");
            webServer.Start(httpListenPort);
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
