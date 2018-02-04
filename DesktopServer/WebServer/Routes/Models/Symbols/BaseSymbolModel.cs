using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Symbols
{
    public class BaseSymbolModel
    {
        public BaseSymbolModel(Symbol symbol)
        {
            Ticker = symbol.Value;
            Market = symbol.ID.Market;
            SecurityType = symbol.SecurityType;
        }

        public string Ticker
        {
            get;
            protected set;
        }

        public string Market
        {
            get;
            protected set;            
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public SecurityType SecurityType
        {
            get;
            protected set;            
        }
    }
}
