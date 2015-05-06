using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockWatch.Entities.Table
{
	public class Eod 
	{
		public string Symbol { get; set; }
		public DateTime Date { get; set; }
		public double Open { get; set; }
		public double High { get; set; }
		public double Low { get; set; }
		public double Close { get; set; }
		public decimal Volume { get; set; }

		/*



		public string ToNormalizedLine()
		{
			return string.Format("{0},{1},{2:0.00},{3:0.00},{4:0.00},{5:0.00},{6}",
				this.Symbol,
				this.Date.ToString("yyyy-MM-dd"),
				this.Open,
				this.High,
				this.Low,
				this.Close,
				this.Volume);
		}

		public string Table
		{
			get { return _table; }
		}*/
	}
}

