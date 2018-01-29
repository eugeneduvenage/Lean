using System;

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

            var server = new DesktopLeanServer();
            var backtestPersistanceManager = new DesktopBacktestPersistanceManager();
            var existingBacktests = backtestPersistanceManager.LoadStoredBacktests();
            var serverData = new DesktopServerData(existingBacktests);
            server.Start(port, new DesktopLeanServerMessageHandler(serverData, backtestPersistanceManager));
            Console.WriteLine("Press any keys to terminate server.");
            Console.ReadKey();
            server.StopServer();
}
    }
}
