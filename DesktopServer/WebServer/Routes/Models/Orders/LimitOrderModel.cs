using System;
using QuantConnect.Orders;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Orders
{
    public class LimitOrderModel : BaseOrderModel
    {
        public LimitOrderModel(Order order)
            : base(order)
        {
        }
    }
}
