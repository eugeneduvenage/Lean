using System;
namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Symbols
{
    public abstract class DerivativeSymbolModel : BaseSymbolModel
    {
        public DerivativeSymbolModel(Symbol symbol)
            : base(symbol)
        {
            if(symbol.HasUnderlying)
            {
                
            }
        }

        public BaseSymbolModel Underlying
        {
            get;
            protected set;
        }
    }
}
