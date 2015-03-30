using System;

namespace StockWatch.Entities.Complex
{
	public struct ComputedEod
	{
		public string Symbol { get; set; }
		public DateTime Date { get; set; }
		public double GL { get; set; }
	}
}

