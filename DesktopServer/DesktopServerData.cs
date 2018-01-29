using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer
{
    public class DesktopServerData : IDesktopServerData
    {
        private ConcurrentDictionary<string, BacktestData> _backTestData;
        public DesktopServerData()
        {
            _backTestData = new ConcurrentDictionary<string, DesktopServer.BacktestData>();
        }

        public ConcurrentDictionary<string, BacktestData> BackTests 
        {
            get
            {
                return _backTestData;    
            }
        }
    }
}
