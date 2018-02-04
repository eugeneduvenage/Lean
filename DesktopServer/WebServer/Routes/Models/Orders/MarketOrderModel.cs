using System;
using QuantConnect.Orders;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Orders
{
    public class MarketOrderModel : BaseOrderModel
    {
        public MarketOrderModel(Order order)
            : base(order)
        {
        }
    }
}
