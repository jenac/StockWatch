using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StockWatch.Utility;

namespace StockWatch.Entities.Complex
{
    public class DailyV001 : DailySummaryDTO
    {
        public static string Version = "001";
        public double ADX14 { get; set; }

        public double SMAShortTerm { get; set; }

        public double SMAMidTerm { get; set; }

        public double SMALongTerm { get; set; }

        public double RSI14 { get; set; }

        public double R30Price { get; set; }

        public double R70Price { get; set; }

        public double VolumePercentAgainstAvg { get; set; }

        public override DailySummary ToDailySummary()
        {
            return new DailySummary
            {
                Symbol = this.Symbol,
                Date = this.Date,
                Version = DailyV001.Version,
                Data = (new XElement("Data",
                    new XElement("ADX14", this.ADX14.MoneyFormat()),
                    new XElement("SMAShortTerm", this.SMAShortTerm.MoneyFormat()),
                    new XElement("SMAMidTerm", this.SMAMidTerm.MoneyFormat()),
                    new XElement("SMALongTerm", this.SMALongTerm.MoneyFormat()),
                    new XElement("RSI14", this.RSI14.MoneyFormat()),
                    new XElement("R30Price", this.R30Price.MoneyFormat()),
                    new XElement("R70Price", this.R70Price.MoneyFormat()),
                    new XElement("VolumePercentAgainstAvg", this.VolumePercentAgainstAvg.MoneyFormat())
                )
                ).ToString()
            };
        }
    }
}
