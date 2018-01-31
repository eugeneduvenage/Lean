using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    public interface IBacktestData 
    {

        string BacktestId { get; }

        BacktestResult Result { get; }

        IEnumerable<string> Logs { get; }
    }

    public class BacktestData : IBacktestData
    {
        private List<string> _logs;
        public BacktestData(string backtestId)
            : this(backtestId, null)
        {

        }

        public BacktestData(string backtestId, BacktestResult result)
        {
            BacktestId = backtestId;
            _logs = new List<string>();
            Result = result;
        }

        public string BacktestId
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
