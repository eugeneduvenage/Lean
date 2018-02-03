using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BacktestState 
    {
        Complete,
        Running
    }

    public interface IBacktestInfo
    {
        string Id { get; }

        BacktestState State { get; }

        Dictionary<string, string> Parameters { get; }

        decimal ProgressPercent { get; }

        DateTime DateRequested { get; }

        DateTime DateFinished { get; }

        double ProcessingTimeInSeconds { get; }

        void SetAsComplete(DateTime dateFinished);

        void UpdateProgress(decimal progress, double processingTime);
    }

    public interface IBacktestData 
    {

        BacktestResult Result { get; }

        IBacktestInfo Info { get; }

        IEnumerable<string> Logs { get; }

    }

    public class BacktestInfo : IBacktestInfo
    {
        public BacktestInfo(string id, BacktestState state, Dictionary<string, string> parameters, decimal progressPercent, DateTime dateRequested,
                            DateTime dateFinished, double processingTimeInSeconds)
        {
            Id = id.Replace(" ", "-").ToLower();
            DisplayId = id;
            State = state;
            Parameters = parameters == null ? new Dictionary<string, string>() : new Dictionary<string, string>(parameters);
            ProgressPercent = progressPercent;
            DateRequested = dateRequested;
            DateFinished = dateFinished;
            ProcessingTimeInSeconds = processingTimeInSeconds;
        }

        public string Id 
        { 
            get;
            protected set;
        }

        public string DisplayId
        {
            get;
            protected set;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public BacktestState State
        {
            get;
            protected set; 
        }

        public Dictionary<string, string> Parameters
        {
            get;
            protected set;
        }

        public decimal ProgressPercent
        {
            get;
            protected set;
        }

        public DateTime DateRequested
        {
            get;
            protected set;
        }

        public DateTime DateFinished
        {
            get;
            protected set;
        }

        public double ProcessingTimeInSeconds
        {
            get;
            protected set;
        }

        public void SetAsComplete(DateTime dateFinished)
        {
            State = BacktestState.Complete;
            DateFinished = dateFinished;
        }

        public void UpdateProgress(decimal progress, double processingTime)
        {
            ProgressPercent = progress * 100.0M;
            ProcessingTimeInSeconds = processingTime;
        }
    }

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
