using System;

namespace StockWatch.Entities
{
	public abstract class IndicatorDTO
	{
		public string Symbol { get; set; }
		public DateTime Date { get; set; }

		public abstract Indicator ToIndicator();
	}
}

