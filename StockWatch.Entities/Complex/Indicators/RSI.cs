using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;
using StockWatch.Utility;
using System.Xml.Linq;

namespace StockWatch.Entities.Complex.Indicators
{
	public class RSI : IndicatorDTO
	{
		public static string Name = "RSI";
		public double PercentGT50 { get; set; }
		public double Avg { get; set; }
		public double LastRSI { get; set; }
		public int TotalDays { get; set; }
		public int MaxContGT50Days { get; set; }
		public double AvgContGT50Days { get; set; }
		public int MaxContLT50Days { get; set; }
		public double AvgContLT50Days { get; set; }
		public int LastContDays { get; set; }

		public override Indicator ToIndicator()
		{
			return new Indicator
			{
				Symbol = this.Symbol,
				Name = RSI.Name,
				Date = this.Date,
                Data = EntityHelper.SerializeToXml<RSI>(this)
			};
		}
	}
}

