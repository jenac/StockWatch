using System;

namespace StockWatch.Entities
{
	public struct DataState
	{
		public string Symbol { get; set; }
		public DateTime? Last { get; set; }
	}
}

