using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QuantConnect.DesktopServer.WebServer.Routes.Models.Symbols;
using QuantConnect.Orders;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models.Orders
{
    public class BaseOrderModel
    {
        public BaseOrderModel(Order order)
        {
            Id = order.Id;
            ContingentId = order.ContingentId;
            BrokerId = order.BrokerId.ToArray();
            Price = order.Price;
            PriceCurrency = order.PriceCurrency;
            Time = order.Time;
            Quantity = order.Quantity;
            Type = order.Type;
            Status = order.Status;
            Duration = order.Duration;
            Direction = order.Direction;
            Tag = order.Tag;
            AbsoluteQuantity = order.AbsoluteQuantity;
            Value = order.Value;

            // parse the symbol
            switch(order.Symbol.SecurityType)
            {
                case SecurityType.Cfd:
                    Symbol = new CfdSymbolModel(order.Symbol);
                    break;
                case SecurityType.Commodity:
                    Symbol = new CommoditySymbolModel(order.Symbol);
                    break;
                case SecurityType.Crypto:
                    Symbol = new CryptoSymbolModel(order.Symbol);
                    break;
                case SecurityType.Equity:
                    Symbol = new EquitySymbolModel(order.Symbol);
                    break;
                case SecurityType.Forex:
                    Symbol = new ForexSymbolModel(order.Symbol);
                    break;
                case SecurityType.Future:
                    Symbol = new FutureSymbolModel(order.Symbol);
                    break;
                case SecurityType.Option:
                    Symbol = new OptionSymbolModel(order.Symbol);
                    break;
            }
        }

        public decimal AbsoluteQuantity
        {
            get;
            protected set;
        }

        public string[] BrokerId
        {
            get;
            protected set;
        }

        public int ContingentId
        {
            get;
            protected set;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderDirection Direction
        {
            get;
            protected set;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderDuration Duration
        {
            get;
            protected set;
        }

        public int Id
        {
            get;
            protected set;
        }

        public decimal Price
        {
            get;
            protected set;
        }

        public string PriceCurrency
        {
            get;
            protected set;            
        }

        public decimal Quantity
        {
            get;
            protected set;            
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus Status
        {
            get;
            protected set;
        }

        public dynamic Symbol
        {
            get;
            protected set;
        }

        public string Tag
        {
            get;
            protected set;
        }

        public DateTime Time
        {
            get;
            protected set;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType Type
        {
            get;
            protected set;            
        }

        public decimal Value
        {
            get;
            protected set;            
        }
    }
}
