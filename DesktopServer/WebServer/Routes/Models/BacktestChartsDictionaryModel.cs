using System;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models
{
    public class BacktestChartsDictionaryModel: Dictionary<string, ChartModel>
    {
        public BacktestChartsDictionaryModel(IDictionary<string, Chart> charts)
        {
            foreach(var kv in charts)
            {
                this.Add(kv.Key, new ChartModel(kv.Value));
            }
        }
    }
}
