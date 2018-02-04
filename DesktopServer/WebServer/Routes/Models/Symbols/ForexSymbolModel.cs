using System;
namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Symbols
{
    public class ForexSymbolModel : BaseSymbolModel
    {
        public ForexSymbolModel(Symbol symbol)
            : base(symbol)
        {
            if (symbol.SecurityType != SecurityType.Forex)
            {
                throw new ArgumentException("Only forex symbols are valid for this context", nameof(symbol));
            }
        }
    }
}
