using System;
using System.Collections.Generic;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    public interface IBacktestData
    {

        BacktestResult Result { get; }

        IBacktestInfo Info { get; }

        IEnumerable<string> Logs { get; }

    }
}
