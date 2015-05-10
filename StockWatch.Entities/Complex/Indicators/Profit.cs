using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;
using StockWatch.Utility;
using System.Xml.Linq;

namespace StockWatch.Entities.Complex.Indicators
{
	public class Profit : IndicatorDTO
	{
		public static string Name = "Profit";
		public double R20Day { get; set; }
		public double R50Day { get; set; }
		public double R100Day { get; set; }
		public double R150Day { get; set; }
		public double R200Day { get; set; }

		public override Indicator ToIndicator()
		{
			return new Indicator
			{
				Symbol = this.Symbol,
				Name = Profit.Name,
				Date = this.Date,
                Data = EntityHelper.SerializeToXml<Profit>(this)
			};
		}
	}
}

