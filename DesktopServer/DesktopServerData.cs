using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer
{
    public class DesktopServerData : IDesktopServerData
    {
        private ConcurrentDictionary<string, BacktestData> _backTestData;

        public DesktopServerData()
            : this(null)
        {
            
        }

        public DesktopServerData(IDictionary<string, BacktestData> existingBacktests)
        {
            _backTestData = (existingBacktests == null ? new ConcurrentDictionary<string, DesktopServer.BacktestData>() :
                             new ConcurrentDictionary<string, DesktopServer.BacktestData>(existingBacktests));
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
