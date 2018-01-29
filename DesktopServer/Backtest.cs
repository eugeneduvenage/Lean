using System;
using System.Collections.Specialized;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    public class BacktestData
    {
        public BacktestData(string algorithmClassName, string backtestId)
        {
            AlgorithmClassName = algorithmClassName;
            BackTestId = backtestId;
            Logs = new StringCollection();
        }

        public string AlgorithmClassName 
        {
            get;
            protected set;
        }

        public string BackTestId
        {
            get;
            protected set;
        }

        public BacktestResult Result 
        {
            get;
            set;
        }

        public StringCollection Logs
        {
            get;
            protected set;
        }
    }
}
