using System;
using QuantConnect.Orders;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Orders
{
    public class MarketOnCloseOrderModel : BaseOrderModel
    {
        public MarketOnCloseOrderModel(Order order)
            : base(order)
        {
        }
    }
}
