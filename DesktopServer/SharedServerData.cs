using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
                _backTestData[algorithmClassName].Add(StandardiseId(backtest.Info.Id), backtest);
            }
        }

        public void UpdateBacktestResults(string algorithmClassName, string backtestId, BacktestResult updatedResult)
        {
            lock (_lockObject)
            {
                _backTestData[algorithmClassName][StandardiseId(backtestId)].UpdateResult(updatedResult);
            }
        }

        public void SetBacktestStateAsCompleted(string algorithmClassName, string backtestId, DateTime dateFinished)
        {
            lock (_lockObject)
            {
                _backTestData[algorithmClassName][StandardiseId(backtestId)].Info.SetAsComplete(dateFinished);
            }            
        }

        public void UpdateProgress(string algorithmClassName, string backtestId, decimal progress, double processingTime)
        {
            lock (_lockObject)
            {
                _backTestData[algorithmClassName][StandardiseId(backtestId)].Info.UpdateProgress(progress, processingTime);
            }              
        }

        public void AppendBacktestLog(string algorithmClassName, string backtestId, string logMessage)
        {
            lock (_lockObject)
            {
                _backTestData[algorithmClassName][StandardiseId(backtestId)].AppendLogMessage(logMessage);
            }            
        }

        public IBacktestData GetBacktestData(string algorithmClassName, string backtestId)
        {
            lock (_lockObject)
            {
                return _backTestData[algorithmClassName][StandardiseId(backtestId)];
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
                                    _backTestData[algorithmClassName].ContainsKey(StandardiseId(backtestId));
            }             
        }

        public string[] GetAlgorithms()
        {
            lock (_lockObject)
            {
                return new List<string>(_backTestData.Keys).ToArray();
            }
        }

        public IBacktestInfo[] GetBacktests(string algorithmClassName)
        {
            lock (_lockObject)
            {
                return _backTestData[algorithmClassName].Select((kv) => kv.Value.Info).ToArray();
            }            
        }

        public IBacktestInfo GetBacktest(string algorithmClassName, string backtestId)
        {
            lock (_lockObject)
            {
                return _backTestData[algorithmClassName][backtestId].Info;
            }               
        }

        public BacktestResult GetBacktestResult(string algorithmClassName, string backtestId)
        {
            lock (_lockObject)
            {
                return _backTestData[algorithmClassName][backtestId].Result;
            }               
        }

        public string StandardiseId(string backtestId)
        {
            return backtestId.Replace(" ", "-").ToLower();
        }
    }
}
