using System;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer
{
    public interface IBacktestInfo
    {
        string Id { get; }

        string DisplayId { get; }

        BacktestState State { get; }

        Dictionary<string, string> Parameters { get; }

        decimal ProgressPercent { get; }

        DateTime DateRequested { get; }

        DateTime DateFinished { get; }

        double ProcessingTimeInSeconds { get; }

        void SetAsComplete(DateTime dateFinished);

        void UpdateProgress(decimal progress, double processingTime);
    }
}
