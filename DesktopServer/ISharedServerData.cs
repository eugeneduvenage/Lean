using System;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    public interface ISharedServerData
    {
        bool HasAlgorithm(string algorithmClassName);

        bool HasBacktest(string algorithmClassName, string backtestId);

        void AddAlgorithm(string algorithmClassName);

        void AddBacktest(string algorithmClassName, BacktestData backtest);

        void UpdateBacktestResults(string algorithmClassName, string backtestId, BacktestResult updateResult);

        void AppendBacktestLog(string algorithmClassName, string backtestId, string logMessage);

        void SetBacktestStateAsCompleted(string algorithmClassName, string backtestId, DateTime dateFinished);

        void UpdateProgress(string algorithmClassName, string backtestId, decimal progress, double processingTime);

        IBacktestData GetBacktestData(string algorithmClassName, string backtestId);

        string[] GetAlgorithms();

        IBacktestInfo[] GetBacktests(string algorithmClassName);

        IBacktestInfo GetBacktest(string algorithmClassName, string backtestId);

        BacktestResult GetBacktestResult(string algorithmClassName, string backtestId);
    }
}
