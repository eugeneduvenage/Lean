using System;
using System.IO;
using Newtonsoft.Json;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    public class JobPersistanceManager
    {
        public JobPersistanceManager()
        {
        }

        public void StoreBacktest(BacktestData backtest) 
        {
            string homeDir = GetHomeDrive();
            string leanDir = Path.Combine(homeDir, "Lean", "algorithms"); //backtest.AlgorithmClassName, backtest.BackTestId
            string algorithmDir = Path.Combine(leanDir, backtest.AlgorithmClassName, "backtests", backtest.BackTestId);
            string logPath = Path.Combine(algorithmDir, "logs.txt");
            string resultsPath = Path.Combine(algorithmDir, "results.json");
            Directory.CreateDirectory(algorithmDir);

            // persist the results json
            var backtestJson = JsonConvert.SerializeObject(backtest.Result, Formatting.Indented);
            using (var writer = new StreamWriter(resultsPath))
            {
                writer.Write(backtestJson);
                writer.Flush();
            }

            // persist the log txt
            using(var writer = new StreamWriter(logPath))
            {
                foreach (var line in backtest.Logs)
                {
                    writer.WriteLine(line);
                }
                writer.Flush();
            }
        }


        private string GetHomeDrive()
        {
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
            Environment.OSVersion.Platform == PlatformID.MacOSX)
            ? Environment.GetEnvironmentVariable("HOME")
            : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            return homePath;
        }
    }
}
