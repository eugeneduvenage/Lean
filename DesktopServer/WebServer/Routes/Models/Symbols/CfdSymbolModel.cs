using System;
namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Symbols
{
    public class CfdSymbolModel : DerivativeSymbolModel
    {
        public CfdSymbolModel(Symbol symbol)
            : base(symbol)
        {
            if (symbol.SecurityType != SecurityType.Cfd)
            {
                throw new ArgumentException("Only cfd symbols are valid for this context", nameof(symbol));
            }
        }
    }
}
