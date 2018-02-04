using System;
using QuantConnect.Orders;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Orders
{
    public class OptionsExerciseOrderModel : BaseOrderModel
    {
        public OptionsExerciseOrderModel(Order order)
            : base(order)
        {
        }
    }
}
