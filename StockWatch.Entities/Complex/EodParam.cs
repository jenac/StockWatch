using System;

namespace StockWatch.Entities.Complex
{
	public struct EodParam
	{
		public string Symbol { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
	}
}

