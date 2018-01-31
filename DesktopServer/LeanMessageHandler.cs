using System;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    public class LeanMessageHandler : ILeanMessageHandler
    {
        private ISharedServerData _sharedServerData;
        private IBacktestPersistanceManager _persistanceManager;
        public LeanMessageHandler(ISharedServerData sharedServerData, IBacktestPersistanceManager persistanceManager)
        {
            _sharedServerData = sharedServerData;
            _persistanceManager = persistanceManager;
        }

        public void Initialize(AlgorithmNodePacket job)
        {
            var backtestIdentifiers = ExtractIdentifiers(job.AlgorithmId);
            _sharedServerData.AddAlgorithm(backtestIdentifiers.AlgorithmClassName);
        }

        public void HandleBacktestResultsPacket(BacktestResultPacket packet)
        {
            Console.WriteLine("HandleBacktestResultsPacket");
            var backtestIds = ExtractIdentifiers(packet.BacktestId);

            if(!_sharedServerData.HasBacktest(backtestIds.AlgorithmClassName, backtestIds.BacktestId))
            {
                _sharedServerData.AddBacktest(backtestIds.AlgorithmClassName, new BacktestData(backtestIds.BacktestId, packet.Results));
            }
            else
            {
                _sharedServerData.UpdateBacktestResults(backtestIds.AlgorithmClassName, backtestIds.BacktestId, packet.Results);    
            }

            if(packet.Progress == 1) 
            {
                var backtest =_sharedServerData.GetBacktestData(backtestIds.AlgorithmClassName, backtestIds.BacktestId);
                _persistanceManager.StoreBacktest(backtestIds.AlgorithmClassName, backtest);
            }
        }

        public void HandleDebugPacket(DebugPacket packet)
        {
            Console.WriteLine("HandleDebugPacket");
        }

        public void HandleErrorPacket(HandledErrorPacket packet)
        {
            Console.WriteLine("HandleErrorPacket");
        }

        public void HandleLogPacket(LogPacket packet)
        {
            Console.WriteLine("HandleLogPacket");
            var backtestIds = ExtractIdentifiers(packet.AlgorithmId);
            _sharedServerData.AppendBacktestLog(backtestIds.AlgorithmClassName, backtestIds.BacktestId, packet.Message);
        }

        public void HandleRuntimeErrorPacket(RuntimeErrorPacket packet)
        {
            Console.WriteLine("HandleRuntimeErrorPacket");
        }

        public BacktestIdentifier ExtractIdentifiers(string combinedIdentifier)
        {
            var identifiers = combinedIdentifier.Split(new string[] { "__" }, StringSplitOptions.RemoveEmptyEntries);
            return new BacktestIdentifier(identifiers[0], identifiers[1]);
        }
    }

    public class BacktestIdentifier 
    {
        public BacktestIdentifier(string algorithmClassName, string backtestId)
        {
            AlgorithmClassName = algorithmClassName;
            BacktestId = backtestId;
        }

        public string AlgorithmClassName 
        {
            get; protected set;
        }

        public string BacktestId
        {
            get; protected set;
        }
    }
}
