using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Symbols
{
    public class OptionSymbolModel : DerivativeSymbolModel
    {
        public OptionSymbolModel(Symbol symbol)
            : base(symbol)
        {
            if (symbol.SecurityType != SecurityType.Option)
            {
                throw new ArgumentException("Only option symbols are valid for this context", nameof(symbol));
            }

            ExpiryDate = symbol.ID.Date;
            StrikePrice = symbol.ID.StrikePrice;
            OptionRight = symbol.ID.OptionRight;
            OptionStyle = symbol.ID.OptionStyle;
        }

        public DateTime ExpiryDate
        {
            get;
            protected set;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public OptionRight OptionRight
        {
            get;
            protected set;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public OptionStyle OptionStyle
        {
            get;
            protected set;
        }

        public decimal StrikePrice
        {
            get;
            protected set;
        }
    }
}
