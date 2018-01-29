using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using QuantConnect.Orders;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    public class DesktopBacktestPersistanceManager : IBacktestPersistanceManager
    {
        private string _leanUserDirectory;
        private string _algorithmsDirectory;
        private string _backtestDirectoryTemplate;

        private const string LEAN_USER_DIR_NAME = "Lean";
        private const string ALGORITHMS_DIR_NAME = "algorithms";
        private const string BACKTESTS_DIR_NAME = "backtests";
        private const string LOGS_OUTPUT_FILE_NAME = "logs.txt";
        private const string RESULTS_OUTPUT_FILE_NAME = "results.json";

        public DesktopBacktestPersistanceManager()
        {
            string homeDir = GetHomeDrive();
            _leanUserDirectory = Path.Combine(homeDir, LEAN_USER_DIR_NAME);
            _algorithmsDirectory = Path.Combine(_leanUserDirectory, ALGORITHMS_DIR_NAME);
            _backtestDirectoryTemplate = Path.Combine(_algorithmsDirectory, "{0}", BACKTESTS_DIR_NAME, "{1}");

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = { new OrderJsonConverter() }
            };
        }

        public void StoreBacktest(BacktestData backtest) 
        {
            string backtestDirectory = string.Format(_backtestDirectoryTemplate, backtest.AlgorithmClassName, backtest.BackTestId);
            string logPath = Path.Combine(backtestDirectory, LOGS_OUTPUT_FILE_NAME);
            string resultsPath = Path.Combine(backtestDirectory, RESULTS_OUTPUT_FILE_NAME);
            Directory.CreateDirectory(backtestDirectory);

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

        public IDictionary<string, BacktestData> LoadStoredBacktests() 
        {
            var storedBacktests = new Dictionary<string, BacktestData>();
            // loop through the algorithms (identified by class type name)
            foreach(var algorithmDirectory in Directory.EnumerateDirectories(_algorithmsDirectory))
            {
                var algorithmClassName = Path.GetFileName(algorithmDirectory);

                // if there is no backtests directory on disk for this algorithm skip it
                if (!HasBacktestDirectory(algorithmClassName))
                {
                    continue;
                }

                // loop through all the backtests for this algorithm (identified by backtest id)
                foreach(var backtestDirectory in Directory.EnumerateDirectories(GetAlgorithmBacktestDirectoryFromClassName(algorithmClassName)))
                {
                    var backtestId = Path.GetFileName(backtestDirectory);
                    var backtestData = new BacktestData(algorithmClassName, backtestId);

                    var backtestLogsFileName = GetAlgorithmBacktestLogsOutputFileName(algorithmClassName, backtestId);
                    if(HasBacktestLogsOutput(backtestLogsFileName))
                    {
                        foreach(var line in File.ReadLines(backtestLogsFileName))
                        {
                            backtestData.Logs.Add(line);      
                        }
                    }

                    var backtestResultsFileName = GetAlgorithmBacktestResultsOutputFileName(algorithmClassName, backtestId);
                    if(HasBacktestResultsOutput(backtestResultsFileName))
                    {
                        var resultsData = File.ReadAllText(backtestResultsFileName);
                        backtestData.Result = JsonConvert.DeserializeObject<BacktestResult>(resultsData);
                    }
                    storedBacktests.Add(string.Format("{0}__{1}", algorithmClassName, backtestId), backtestData);
                }

            }

            return storedBacktests;
        }

        private bool HasBacktestDirectory(string algorithmClassName)
        {
            return Directory.Exists(GetAlgorithmBacktestDirectoryFromClassName(algorithmClassName));
        }

        private bool HasBacktestLogsOutput(string backtestLogsFileName)
        {
            return File.Exists(backtestLogsFileName);
        }

        private bool HasBacktestResultsOutput(string backtestResultsFileName)
        {
            return File.Exists(backtestResultsFileName);
        }

        private string GetAlgorithmBacktestDirectoryFromClassName(string algorithmClassName)
        {
            return Path.Combine(_algorithmsDirectory, algorithmClassName, BACKTESTS_DIR_NAME);
        }

        private string GetAlgorithmBacktestLogsOutputFileName(string algorithmClassName, string backtestId)
        {
            return Path.Combine(GetAlgorithmBacktestDirectoryFromClassName(algorithmClassName), backtestId, LOGS_OUTPUT_FILE_NAME);
        }

        private string GetAlgorithmBacktestResultsOutputFileName(string algorithmClassName, string backtestId)
        {
            return Path.Combine(GetAlgorithmBacktestDirectoryFromClassName(algorithmClassName), backtestId, RESULTS_OUTPUT_FILE_NAME);
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
