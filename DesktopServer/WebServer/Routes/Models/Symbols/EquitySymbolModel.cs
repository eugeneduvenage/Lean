using System;
namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Symbols
{
    public class EquitySymbolModel : BaseSymbolModel
    {
        public EquitySymbolModel(Symbol symbol)
            : base(symbol)
        {
            if(symbol.SecurityType != SecurityType.Equity)
            {
                throw new ArgumentException("Only equity symbols are valid for this context", nameof(symbol));
            }
            FirstTradedDate = symbol.ID.Date;
        }

        public DateTime FirstTradedDate
        {
            get;
            protected set;
        }
    }
}
