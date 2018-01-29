using System;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    public interface IDesktopLeanMessageHandler
    {
        void Initialize(AlgorithmNodePacket job);

        void HandleErrorPacket(HandledErrorPacket packet);

        void HandleRuntimeErrorPacket(RuntimeErrorPacket packet);

        void HandleLogPacket(LogPacket packet);

        void HandleDebugPacket(DebugPacket packet);

        void HandleBacktestResultsPacket(BacktestResultPacket packet);        
    }
}
