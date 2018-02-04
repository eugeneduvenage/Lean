using System;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models
{
    public class RuntimeStatisticsModel : Dictionary<string, string>
    {
        public RuntimeStatisticsModel(IDictionary<string, string> runtimeStatistics)
        {
            foreach(var kv in runtimeStatistics)
            {
                // TODO: 
                // add camelcase named version of the key
            }
        }
    }
}
