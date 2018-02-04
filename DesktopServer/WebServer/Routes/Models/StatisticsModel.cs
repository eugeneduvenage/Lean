using System;
using System.Collections.Generic;

namespace QuantConnect.DesktopServer.WebServer.Routes.Models
{
    public class StatisticsModel
    {
        public StatisticsModel(IDictionary<string, string> statistics)
        {
            if (statistics.ContainsKey("Total Trades"))
            {
                TotalTrades = statistics["Total Trades"];
            }
            if (statistics.ContainsKey("Average Win"))
            {
                AverageWinPercent = statistics["Average Win"];
            }
            if (statistics.ContainsKey("Average Loss"))
            {
                AverageLossPercent = statistics["Average Loss"];
            }
            if (statistics.ContainsKey("Compounding Annual Return"))
            {
                CompoundingAnnualReturnPercent = statistics["Compounding Annual Return"];
            }
            if (statistics.ContainsKey("Drawdown"))
            {
                DrawdownPercent = statistics["Drawdown"];
            }
            if (statistics.ContainsKey("Net Profit"))
            {
                NetProfitPercent = statistics["Net Profit"];
            }
            if (statistics.ContainsKey("Sharpe Ratio"))
            {
                SharpeRatio = statistics["Sharpe Ratio"];
            }
            if (statistics.ContainsKey("Loss Rate"))
            {
                LossRatePercent = statistics["Loss Rate"];
            }
            if (statistics.ContainsKey("Win Rate"))
            {
                WinRatePercent = statistics["Win Rate"];
            }
            if (statistics.ContainsKey("Profit-Loss Ratio"))
            {
                ProfitLossRatio = statistics["Profit-Loss Ratio"];
            }
            if (statistics.ContainsKey("Alpha"))
            {
                Alpha = statistics["Alpha"];
            }
            if (statistics.ContainsKey("Beta"))
            {
                Beta = statistics["Beta"];
            }
            if (statistics.ContainsKey("Annual Standard Deviation"))
            {
                AnnualStandardDeviation = statistics["Annual Standard Deviation"];
            }
            if (statistics.ContainsKey("Annual Variance"))
            {
                AnnualVariance = statistics["Annual Variance"];
            }
            if (statistics.ContainsKey("Information Ratio"))
            {
                InformationRatio = statistics["Information Ratio"];
            }
            if (statistics.ContainsKey("Tracking Error"))
            {
                TrackingError = statistics["Tracking Error"];
            }
            if (statistics.ContainsKey("Treynor Ratio"))
            {
                TreynorRatio = statistics["Treynor Ratio"];
            }
            if (statistics.ContainsKey("Total Fees"))
            {
                TotalFees = statistics["Total Fees"];
            }
        }

        public string Alpha
        {
            get;
            protected set;
        }

        public string AnnualStandardDeviation
        {
            get;
            protected set;
        }

        public string AnnualVariance
        {
            get;
            protected set;
        }

        public string AverageLossPercent
        {
            get;
            protected set;
        }

        public string AverageWinPercent
        {
            get;
            protected set;
        }

        public string Beta
        {
            get;
            protected set;
        }

        public string CompoundingAnnualReturnPercent
        {
            get;
            protected set;
        }

        public string DrawdownPercent
        {
            get;
            protected set;
        }

        public string Expectancy
        {
            get;
            protected set;
        }

        public string InformationRatio
        {
            get;
            protected set;
        }

        public string LossRatePercent
        {
            get;
            protected set;
        }

        public string NetProfitPercent
        {
            get;
            protected set;
        }

        public string ProfitLossRatio
        {
            get;
            protected set;
        }

        public string SharpeRatio
        {
            get;
            protected set;
        }

        public string TotalFees
        {
            get;
            protected set;
        }

        public string TotalTrades
        {
            get;
            protected set;
        }

        public string TrackingError
        {
            get;
            protected set;
        }

		public string TreynorRatio
		{
			get;
			protected set;
		}

        public string WinRatePercent
        {
            get;
            protected set;
        }
    }
}
