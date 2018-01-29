using System;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer
{
    public interface IBacktestPersistanceManager
    {
        void StoreBacktest(BacktestData backtest);

        IDictionary<string, BacktestData> LoadStoredBacktests();
    }
}
