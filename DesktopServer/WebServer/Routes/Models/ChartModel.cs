using System;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models
{
    public class ChartModel
    {
            public ChartModel(Chart chart)
        {
            Name = chart.Name;
            Series = new Dictionary<string, ChartSeries>();
            foreach(var series in chart.Series)
            {
                Series.Add(series.Key, new ChartSeries(series.Value));
            }
        }

        public string Name
        {
            get;
            protected set;
        }

        public IDictionary<string, ChartSeries> Series
        {
            get;
            protected set;
        }
    }
}
