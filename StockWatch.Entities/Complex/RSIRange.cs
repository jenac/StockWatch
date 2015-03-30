using StockWatch.Entities.Table;
using StockWatch.Utility;
using System.Xml.Linq;
namespace StockWatch.Entities.Complex
{
	public class RSIRange : IndicatorDTO
	{
		/// <summary>
		/// Min: min RSI
		/// L5: 5% days in the low range
		/// H5: 5% days in the low range
		/// </summary>
		public static string Name = "RSI Range";
		public double Min { get; set; }
		public double Max { get; set; }
		public double L5 { get; set; }
		public double H5 { get; set; }
		public double L10 { get; set; }
		public double H10 { get; set; }
		public double L15 { get; set; }
		public double H15 { get; set; }


		public override Indicator ToIndicator()
		{
			return new Indicator
			{
				Symbol = this.Symbol,
				Name = RSIRange.Name,
				Date = this.Date,
				Data = (new XElement("Data",
					new XElement("Min", this.Min.MoneyFormat()),
					new XElement("Max", this.Max.MoneyFormat()),
					new XElement("L5", this.L5.MoneyFormat()),
					new XElement("H5", this.H5.MoneyFormat()),
					new XElement("L10", this.L10.MoneyFormat()),
					new XElement("H10", this.H10.MoneyFormat()),
					new XElement("L15", this.L15.MoneyFormat()),
					new XElement("H15", this.H15.MoneyFormat()))).ToString()
			};
		}
	}
}

