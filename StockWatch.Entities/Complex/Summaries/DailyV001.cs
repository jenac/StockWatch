using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StockWatch.Utility;
using StockWatch.Entities.Helper;

namespace StockWatch.Entities.Complex.Summaries
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
                Data = EntityHelper.SerializeToXml<DailyV001>(this)
            };
        }
    }
}
