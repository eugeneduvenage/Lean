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

        IBacktestData GetBacktestData(string algorithmClassName, string backtestId);

        string[] GetAlgorithms();

        string[] GetBacktests(string algorithmClassName);
    }
}
