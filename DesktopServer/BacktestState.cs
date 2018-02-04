using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace QuantConnect.DesktopServer
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BacktestState
    {
        Complete,
        Running
    }
}
