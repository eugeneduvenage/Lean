using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QuantConnect.Util;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models
{
    public class ChartSeriesModel
    {
        public ChartSeriesModel(Series chartSeries)
        {
            Name = chartSeries.Name;
            Unit = chartSeries.Unit;
            Index = chartSeries.Index;
            Values = chartSeries.Values;
            SeriesType = chartSeries.SeriesType;
            Color = chartSeries.Color;
            ScatterMarkerSymbol = (ChartScatterMarkerSymbol)chartSeries.ScatterMarkerSymbol;
        }

        [JsonConverter(typeof(ColorJsonConverter))]
        public Color Color
        {
            get;
            protected set;
        }

        public int Index
        {
            get;
            protected set;
        }

        public string Name
        {
            get;
            protected set;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public ChartScatterMarkerSymbol ScatterMarkerSymbol
        {
            get;
            protected set;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public SeriesType SeriesType
        {
            get;
            protected set;
        }

        public string Unit
        {
            get;
            protected set;
        }

        public List<ChartPoint> Values
        {
            get;
            protected set;
        }
    }

    public enum ChartScatterMarkerSymbol
    {
        None,
        Circle,
        Square,
        Diamond,
        Triangle,
        TriangleDown
    }
}
