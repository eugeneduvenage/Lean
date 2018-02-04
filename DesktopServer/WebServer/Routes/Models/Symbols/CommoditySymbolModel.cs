using System;
namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Symbols
{
    public class CommoditySymbolModel : BaseSymbolModel
    {
        public CommoditySymbolModel(Symbol symbol)
            : base(symbol)
        {
            if (symbol.SecurityType != SecurityType.Commodity)
            {
                throw new ArgumentException("Only commodity symbols are valid for this context", nameof(symbol));
            }
        }
    }
}
