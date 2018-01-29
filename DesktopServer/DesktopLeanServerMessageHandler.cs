using System;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    public class DesktopLeanServerMessageHandler : IDesktopLeanMessageHandler
    {
        private IDesktopServerData _desktopServerData;
        private IBacktestPersistanceManager _persistanceManager;
        public DesktopLeanServerMessageHandler(IDesktopServerData desktopServerData, IBacktestPersistanceManager persistanceManager)
        {
            _desktopServerData = desktopServerData;
            _persistanceManager = persistanceManager;
        }

        public void Initialize(AlgorithmNodePacket job)
        {
            Console.WriteLine("Init");
            Console.WriteLine("AlgoId: " + job.AlgorithmId);
            Console.WriteLine("SessionId: " + job.SessionId);
            Console.WriteLine("ProjectId: " + job.ProjectId);
            Console.WriteLine("CompileId: " + job.CompileId);
            Console.WriteLine("UserId: " + job.UserId);

            var splits = job.AlgorithmId.Split(new string[] { "__" }, StringSplitOptions.RemoveEmptyEntries);
            _desktopServerData.BackTests.TryAdd(job.AlgorithmId, new BacktestData(splits[0], splits[1]));
        }

        public void HandleBacktestResultsPacket(BacktestResultPacket packet)
        {
            Console.WriteLine("HandleBacktestResultsPacket");

            BacktestData backtest;
            _desktopServerData.BackTests.TryGetValue(packet.BacktestId, out backtest);
            backtest.Result = packet.Results;

            _desktopServerData.BackTests.AddOrUpdate(packet.BacktestId, backtest);

            if(packet.Progress == 1) 
            {
                _persistanceManager.StoreBacktest(backtest);
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
            Console.WriteLine(packet.Message);
            BacktestData backtest;
            _desktopServerData.BackTests.TryGetValue(packet.AlgorithmId, out backtest);
            backtest.Logs.Add(packet.Message);
            _desktopServerData.BackTests.AddOrUpdate(packet.AlgorithmId, backtest);
        }

        public void HandleRuntimeErrorPacket(RuntimeErrorPacket packet)
        {
            Console.WriteLine("HandleRuntimeErrorPacket");
        }
    }
}
