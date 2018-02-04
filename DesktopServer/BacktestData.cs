using System;
using System.Collections.Generic;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    public class BacktestData : IBacktestData
    {
        private List<string> _logs;
        public BacktestData(IBacktestInfo backtestInfo)
            : this(backtestInfo, null)
        {

        }

        public BacktestData(IBacktestInfo backtestInfo, BacktestResult result)
        {
            _logs = new List<string>();
            Result = result;
            Info = backtestInfo;
        }

        public IBacktestInfo Info
        {
            get;
            protected set;
        }

        public BacktestResult Result
        {
            get;
            protected set;
        }

        public IEnumerable<string> Logs
        {
            get
            {
                return _logs;
            }
        }

        public void UpdateResult(BacktestResult updatedResult)
        {
            this.Result = updatedResult;
        }

        public void AppendLogMessage(string message)
        {
            this._logs.Add(message);
        }

        public void AppendLogMessages(IEnumerable<string> messages)
        {
            this._logs.AddRange(messages);
        }
    }
}
