using System;
using QuantConnect.Orders;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Orders
{
    public class MarketOnOpenOrderModel : BaseOrderModel
    {
        public MarketOnOpenOrderModel(Order order)
            : base(order)
        {
        }
    }
}
