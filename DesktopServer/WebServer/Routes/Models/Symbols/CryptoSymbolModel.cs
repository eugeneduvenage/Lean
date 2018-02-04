using System;
namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Symbols
{
    public class CryptoSymbolModel : BaseSymbolModel
    {
        public CryptoSymbolModel(Symbol symbol)
            : base(symbol)
        {
            if (symbol.SecurityType != SecurityType.Crypto)
            {
                throw new ArgumentException("Only crypto symbols are valid for this context", nameof(symbol));
            }
        }
    }
}
