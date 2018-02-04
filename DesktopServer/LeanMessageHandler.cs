using System;
using System.Collections.Generic;
using QuantConnect.Logging;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    public class LeanMessageHandler : ILeanMessageHandler
    {
        private ISharedServerData _sharedServerData;
        private IBacktestPersistenceManager _persistanceManager;
        private Dictionary<string, Dictionary<string, string>> _algorithmBackTestParameters;
        public LeanMessageHandler(ISharedServerData sharedServerData, IBacktestPersistenceManager persistanceManager)
        {
            _sharedServerData = sharedServerData;
            _persistanceManager = persistanceManager;
            _algorithmBackTestParameters = new Dictionary<string, Dictionary<string, string>>();
        }

        public void Initialize(AlgorithmNodePacket job)
        {
            var backtestIdentifiers = ExtractIdentifiers(job.AlgorithmId);
            _sharedServerData.AddAlgorithm(backtestIdentifiers.AlgorithmClassName);
            _algorithmBackTestParameters.Add(backtestIdentifiers.BacktestId, job.Parameters);
        }

        public void HandleBacktestResultsPacket(BacktestResultPacket packet)
        {
            Log.Trace("HandleBacktestResultsPacket");;
            var backtestIds = ExtractIdentifiers(packet.BacktestId);

            if(!_sharedServerData.HasBacktest(backtestIds.AlgorithmClassName, backtestIds.BacktestId))
            {
                var parameters = _algorithmBackTestParameters[backtestIds.BacktestId];
                var backtestInfo = new BacktestInfo(backtestIds.BacktestId, BacktestState.Running, parameters, packet.Progress * 100.0M, packet.DateRequested,
                                                    packet.DateFinished, packet.ProcessingTime);
                _sharedServerData.AddBacktest(backtestIds.AlgorithmClassName, new BacktestData(backtestInfo, packet.Results));
            }
            else
            {
                _sharedServerData.UpdateBacktestResults(backtestIds.AlgorithmClassName, backtestIds.BacktestId, packet.Results);    
                _sharedServerData.UpdateProgress(backtestIds.AlgorithmClassName, backtestIds.BacktestId, packet.Progress, packet.ProcessingTime);
            }

            if(packet.Progress == 1) 
            {
                var backtest =_sharedServerData.GetBacktestData(backtestIds.AlgorithmClassName, backtestIds.BacktestId);
                _sharedServerData.SetBacktestStateAsCompleted(backtestIds.AlgorithmClassName, backtestIds.BacktestId, packet.DateFinished);
                _persistanceManager.StoreBacktest(backtestIds.AlgorithmClassName, backtest);
            }
        }

        public void HandleDebugPacket(DebugPacket packet)
        {
            Log.Trace("HandleDebugPacket");
        }

        public void HandleErrorPacket(HandledErrorPacket packet)
        {
            Log.Trace("HandleErrorPacket");
        }

        public void HandleLogPacket(LogPacket packet)
        {
            Log.Trace("HandleLogPacket");
            var backtestIds = ExtractIdentifiers(packet.AlgorithmId);
            _sharedServerData.AppendBacktestLog(backtestIds.AlgorithmClassName, backtestIds.BacktestId, packet.Message);
        }

        public void HandleRuntimeErrorPacket(RuntimeErrorPacket packet)
        {
            Log.Trace("HandleRuntimeErrorPacket");
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
