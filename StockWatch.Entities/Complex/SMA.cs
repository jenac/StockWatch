using StockWatch.Entities.Table;
using StockWatch.Utility;
using System.Xml.Linq;

namespace StockWatch.Entities.Complex
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
				Data = (new XElement("Data",
                    new XElement("SMA5", this.SMA5.MoneyFormat()),
                    new XElement("SMA10", this.SMA10.MoneyFormat()),
                    new XElement("SMA20", this.SMA20.MoneyFormat()),
					new XElement("SMA50", this.SMA50.MoneyFormat()),
					new XElement("SMA200", this.SMA200.MoneyFormat()))).ToString()
			};
		}
	}
}

