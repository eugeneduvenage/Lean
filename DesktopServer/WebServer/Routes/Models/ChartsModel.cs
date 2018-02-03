using System;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models
{
    public class ChartsModel: Dictionary<string, ChartModel>
    {
        public ChartsModel(IDictionary<string, Chart> charts)
        {
            foreach(var kv in charts)
            {
                this.Add(kv.Key, new ChartModel(kv.Value));
            }
        }
    }
}
