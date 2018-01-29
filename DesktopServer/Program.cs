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
            var serverData = new DesktopServerData();
            server.Start(port, new DesktopLeanServerMessageHandler(serverData));
            Console.WriteLine("Press any keys to terminate server.");
            Console.ReadKey();
            server.StopServer();
}
    }
}
