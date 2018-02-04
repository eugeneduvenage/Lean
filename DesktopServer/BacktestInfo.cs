using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer
{
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
}
