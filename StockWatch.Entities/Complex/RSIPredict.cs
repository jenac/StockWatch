using StockWatch.Entities.Table;
using StockWatch.Utility;
using System.Xml.Linq;

namespace StockWatch.Entities.Complex
{
	public class RSIPredict : IndicatorDTO
	{
		public static string Name = "RSI Predict";
		public double PredictRsi30Price { get; set; }
		public double PredictRsi50Price { get; set; }
		public double PredictRsi70Price { get; set; }

		public override Indicator ToIndicator()
		{
			return new Indicator
			{
				Symbol = this.Symbol,
				Name = RSIPredict.Name,
				Date = this.Date,
				Data = (new XElement("Data",
					new XElement("PredictRsi30Price", this.PredictRsi30Price.MoneyFormat()),
					new XElement("PredictRsi50Price", this.PredictRsi50Price.MoneyFormat()),
					new XElement("PredictRsi70Price", this.PredictRsi70Price.MoneyFormat()))).ToString()
			};
		}
	}
}

