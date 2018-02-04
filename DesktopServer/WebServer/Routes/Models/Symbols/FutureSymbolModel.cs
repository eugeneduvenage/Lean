using System;
namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Symbols
{
    public class FutureSymbolModel : DerivativeSymbolModel
    {
        public FutureSymbolModel(Symbol symbol)
            : base(symbol)
        {
            if (symbol.SecurityType != SecurityType.Future)
            {
                throw new ArgumentException("Only future symbols are valid for this context", nameof(symbol));
            }

            SettlementDate = symbol.ID.Date;
        }

        public DateTime SettlementDate
        {
            get;
            protected set;
        }
    }
}
