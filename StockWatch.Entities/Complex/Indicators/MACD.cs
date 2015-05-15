using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.Entities.Complex.Indicators
{
    public class MACD : IndicatorDTO
    {
        public const string Name = "MACD";
        public double MacdValue { get; set; }
        public double MacdSingal { get; set; }
        public double MacdHIST { get; set; }

        public override Indicator ToIndicator()
        {
            return new Indicator
            {
                Symbol = this.Symbol,
                Name = MACD.Name,
                Date = this.Date,
                Data = EntityHelper.SerializeToXml<MACD>(this)
            };
        }
    }
}
