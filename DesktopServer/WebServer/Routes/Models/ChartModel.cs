using System;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models
{
    public class ChartModel
    {
            public ChartModel(Chart chart)
        {
            Name = chart.Name;
            Series = new Dictionary<string, ChartSeriesModel>();
            foreach(var series in chart.Series)
            {
                Series.Add(series.Key, new ChartSeriesModel(series.Value));
            }
        }

        public string Name
        {
            get;
            protected set;
        }

        public IDictionary<string, ChartSeriesModel> Series
        {
            get;
            protected set;
        }
    }
}
