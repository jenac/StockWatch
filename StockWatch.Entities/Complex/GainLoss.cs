using StockWatch.Entities.Table;
using StockWatch.Utility;
using System.Xml.Linq;

namespace StockWatch.Entities.Complex
{
	public class GainLoss : IndicatorDTO
	{
		public static string Name = "GainLoss";
		public int MaxContGainDays { get; set; }
		public double AvgContGainDays { get; set; }
		public int MaxContLossDays { get; set; }
		public double AvgContLossDays { get; set; }
		public int LastGLContDays { get; set; }

		public override Indicator ToIndicator()
		{
			return new Indicator
			{
				Symbol = this.Symbol,
				Name = GainLoss.Name,
				Date = this.Date,
				Data = (new XElement("Data",
					new XElement("MaxContGainDays", this.MaxContGainDays),
					new XElement("AvgContGainDays", this.AvgContGainDays.MoneyFormat()),
					new XElement("MaxContLossDays", this.MaxContLossDays),
					new XElement("AvgContLossDays", this.AvgContLossDays.MoneyFormat()),
					new XElement("LastGLContDays", this.LastGLContDays))).ToString()
			};
		}
	}
}

