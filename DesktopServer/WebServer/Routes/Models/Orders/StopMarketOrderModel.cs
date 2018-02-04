using System;
using QuantConnect.Orders;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Orders
{
    public class StopMarketOrderModel : BaseOrderModel
    {
        public StopMarketOrderModel(Order order)
            : base(order)
        {
            if(order.Type != OrderType.StopMarket)
            {
                throw new ArgumentException("Only stop market orders are valid for this context", nameof(order));
            }

            StopPrice = ((QuantConnect.Orders.StopMarketOrder)order).StopPrice;
        }

        public decimal StopPrice
        {
            get;
            protected set;
        }
    }
}
