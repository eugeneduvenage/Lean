using System;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models
{
    public class BacktestModel
    {
        public BacktestModel(IBacktestInfo backtestInfo)
        {
            Id = backtestInfo.Id;
            DisplayId = backtestInfo.DisplayId;
            State = backtestInfo.State;
            Parameters = new Dictionary<string, string>(backtestInfo.Parameters);
            ProgressPercent = backtestInfo.ProgressPercent;
            DateRequested = backtestInfo.DateRequested;
            DateFinished = backtestInfo.DateFinished;
            ProcessingTimeInSeconds = backtestInfo.ProcessingTimeInSeconds;
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

        public string DisplayId
        {
            get;
            protected set;
        }

        public string Id
        {
            get;
            protected set;
        }

        public Dictionary<string, string> Parameters 
        { 
            get;
            protected set;
        }

        public double ProcessingTimeInSeconds
        {
            get;
            protected set;
        }

        public decimal ProgressPercent 
        { 
            get;
            protected set;
        }

        public BacktestState State
        {
            get;
            protected set;
        }
    }
}
