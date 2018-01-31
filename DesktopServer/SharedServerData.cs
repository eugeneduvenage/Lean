using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    public class SharedServerData : ISharedServerData
    {
        private readonly object _lockObject = new object();
        private Dictionary<string, Dictionary<string, BacktestData>> _backTestData;

        public SharedServerData()
            : this(null)
        {
            
        }

        public SharedServerData(Dictionary<string, Dictionary<string, BacktestData>> existingBacktests)
        {
            if (existingBacktests != null)
            {
                _backTestData = existingBacktests;
            } 
            else 
            {
                _backTestData = new Dictionary<string, Dictionary<string, BacktestData>>();                
            }
        }

        public void AddAlgorithm(string algorithmClassName)
        {
            lock (_lockObject)
            {
                if (!_backTestData.ContainsKey(algorithmClassName))
                {
                    _backTestData.Add(algorithmClassName, new Dictionary<string, BacktestData>());
                }
            }
        }

        public void AddBacktest(string algorithmClassName, BacktestData backtest)
        {
            lock (_lockObject)
            {
                if (!_backTestData.ContainsKey(algorithmClassName))
                {
                    throw new ArgumentException("Specified algorithm does not exist, cannot add backtest");
                }
                _backTestData[algorithmClassName].Add(backtest.BacktestId, backtest);
            }
        }

        public void UpdateBacktestResults(string algorithmClassName, string backtestId, BacktestResult updatedResult)
        {
            lock (_lockObject)
            {
                _backTestData[algorithmClassName][backtestId].UpdateResult(updatedResult);
            }
        }

        public void AppendBacktestLog(string algorithmClassName, string backtestId, string logMessage)
        {
            lock (_lockObject)
            {
                _backTestData[algorithmClassName][backtestId].AppendLogMessage(logMessage);
            }            
        }

        public IBacktestData GetBacktestData(string algorithmClassName, string backtestId)
        {
            lock (_lockObject)
            {
                return _backTestData[algorithmClassName][backtestId];
            }
        }

        public bool HasAlgorithm(string algorithmClassName)
        {
            lock (_lockObject)
            {
                return _backTestData.ContainsKey(algorithmClassName);
            }            
        }

        public bool HasBacktest(string algorithmClassName, string backtestId)
        {
            lock (_lockObject)
            {
                return _backTestData.ContainsKey(algorithmClassName) &&
                       _backTestData[algorithmClassName].ContainsKey(backtestId);
            }             
        }

        public string[] GetAlgorithms()
        {
            lock (_lockObject)
            {
                return new List<string>(_backTestData.Keys).ToArray();
            }
        }

        public string[] GetBacktests(string algorithmClassName)
        {
            lock (_lockObject)
            {
                return new List<string>(_backTestData[algorithmClassName].Keys).ToArray();
            }            
        }
    }
}
