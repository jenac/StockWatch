using StockWatch.Entities.Table;
using StockWatch.Utility;
using System.Xml.Linq;

namespace StockWatch.Entities.Complex
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
				Data = (new XElement("Data",
					new XElement("R20Day", this.R20Day.MoneyFormat()),
					new XElement("R50Day", this.R50Day.MoneyFormat()),
					new XElement("R100Day", this.R100Day.MoneyFormat()),
					new XElement("R150Day", this.R150Day.MoneyFormat()),
					new XElement("R200Day", this.R200Day.MoneyFormat()))).ToString()
			};
		}
	}
}

