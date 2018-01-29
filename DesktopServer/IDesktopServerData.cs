using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer
{
    public interface IDesktopServerData
    {
        ConcurrentDictionary<string, BacktestData> BackTests 
        {
            get;
        }
    }
}
