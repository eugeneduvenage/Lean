using System;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer
{
    public interface IBacktestPersistanceManager
    {
        void StoreBacktest(string algorithmClassName, IBacktestData backtest);

        Dictionary<string, Dictionary<string, BacktestData>> LoadStoredBacktests();
    }
}
