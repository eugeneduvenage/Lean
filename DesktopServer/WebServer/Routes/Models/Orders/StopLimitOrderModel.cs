using System;
using QuantConnect.Orders;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Orders
{
    public class StopLimitOrderModel : BaseOrderModel
    {
        public StopLimitOrderModel(Order order)
            : base(order)
        {
            if (order.Type != OrderType.StopLimit)
            {
                throw new ArgumentException("Only stop limit orders are valid for this context", nameof(order));
            }

            StopPrice = ((QuantConnect.Orders.StopLimitOrder)order).StopPrice;
            LimitPrice = ((QuantConnect.Orders.StopLimitOrder)order).LimitPrice;
            StopTriggered = ((QuantConnect.Orders.StopLimitOrder)order).StopTriggered;
        }

        public decimal StopPrice
        {
            get;
            protected set;
        }

        public decimal LimitPrice
        {
            get;
            protected set;
        }

        public bool StopTriggered
        {
            get;
            protected set;
        }
    }
}
