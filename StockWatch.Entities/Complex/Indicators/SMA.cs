using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;
using StockWatch.Utility;
using System.Xml.Linq;

namespace StockWatch.Entities.Complex.Indicators
{
	public class SMA : IndicatorDTO
	{
		public static string Name = "SMA";

        public double SMA5 { get; set; }
        public double SMA10 { get; set; }
        public double SMA20 { get; set; }
		public double SMA50 { get; set; }
		public double SMA200 { get; set; }

		public override Indicator ToIndicator()
		{
			return new Indicator
			{
				Symbol = this.Symbol,
				Name = SMA.Name,
				Date = this.Date,
                Data = EntityHelper.SerializeToXml<SMA>(this)
			};
		}
	}
}

