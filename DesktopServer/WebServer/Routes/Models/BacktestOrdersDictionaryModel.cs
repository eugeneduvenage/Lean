using System;
using System.Collections.Generic;
using QuantConnect.DesktopServer.WebServer.Routes.Models.Orders;
using QuantConnect.Orders;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models
{
    public class BacktestOrdersDictionaryModel: Dictionary<int, BaseOrderModel>
    {
        public BacktestOrdersDictionaryModel(IDictionary<int, Order> orders)
        {
            foreach (var kv in orders)
            {
                // do this per type
                switch(kv.Value.Type)
                {
                    case OrderType.Limit:
                        this.Add(kv.Key, new Orders.LimitOrderModel(kv.Value));
                        break;
                    case OrderType.Market:
                        this.Add(kv.Key, new Orders.MarketOrderModel(kv.Value));
                        break;
                    case OrderType.MarketOnClose:
                        this.Add(kv.Key, new Orders.MarketOnCloseOrderModel(kv.Value));
                        break;
                    case OrderType.MarketOnOpen:
                        this.Add(kv.Key, new Orders.MarketOnOpenOrderModel(kv.Value));
                        break;
                    case OrderType.OptionExercise:
                        this.Add(kv.Key, new Orders.OptionsExerciseOrderModel(kv.Value));
                        break;
                    case OrderType.StopLimit:
                        this.Add(kv.Key, new Orders.StopLimitOrderModel(kv.Value));
                        break;
                    case OrderType.StopMarket:
                        this.Add(kv.Key, new Orders.StopMarketOrderModel(kv.Value));
                        break;
                }
            }
        }
    }
}
