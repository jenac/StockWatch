using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StockWatch.Utility;
using StockWatch.Entities.Helper;

namespace StockWatch.Entities.Complex.Indicators
{
    public class ADX : IndicatorDTO
    {
        public const string Name = "ADX";
        
        public double ADX14 { get; set; }
        public override Indicator ToIndicator()
        {
            return new Indicator
            {
                Symbol = this.Symbol,
                Name = ADX.Name,
                Date = this.Date,
                Data = EntityHelper.SerializeToXml<ADX>(this)
            };
        }
    }

    
		
		
}
