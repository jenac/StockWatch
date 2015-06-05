using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.Entities.Complex.Indicators
{
    public class BollingerBands : IndicatorDTO
    {
        public const string Name = "BollingerBands";
        public double Upper { get; set; }
        public double Middle { get; set; }
        public double Lower { get; set; }
        public double ChannelHight { get; set; }
        public double ChannelPercent { get; set; }

        public override Indicator ToIndicator()
        {
            return new Indicator
            {
                Symbol = this.Symbol,
                Name = BollingerBands.Name,
                Date = this.Date,
                Data = EntityHelper.SerializeToXml<BollingerBands>(this)
            };
        }
    }
}
