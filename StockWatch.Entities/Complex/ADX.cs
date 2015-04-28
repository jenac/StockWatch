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
    public class ADX : IndicatorDTO
    {
        public static string Name = "ADX";
        public double ADX14 { get; set; }
        public override Indicator ToIndicator()
        {
            return new Indicator
            {
                Symbol = this.Symbol,
                Name = ADX.Name,
                Date = this.Date,
                Data = (new XElement("Data",
                    new XElement("ADX14", this.ADX14.MoneyFormat())
                )
                ).ToString()
            };
        }
    }

    
		
		
}
