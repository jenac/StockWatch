using System;
using System.Xml.Linq;
using StockWatch.Utility;

namespace StockWatch.Entities
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
				Data = (new XElement("Data",
					new XElement("PercentGT50", this.PercentGT50.MoneyFormat()),
					new XElement("Avg", this.Avg.MoneyFormat()),
					new XElement("LastRSI", this.LastRSI.MoneyFormat()),
					new XElement("TotalDays", this.TotalDays),
					new XElement("MaxContGT50Days", this.MaxContGT50Days),
					new XElement("AvgContGT50Days", this.AvgContGT50Days.MoneyFormat()),
					new XElement("MaxContLT50Days", this.MaxContLT50Days),
					new XElement("AvgContLT50Days", this.AvgContLT50Days.MoneyFormat()),
					new XElement("LastContDays", this.LastContDays)
				)
				).ToString()
			};
		}
	}
}

