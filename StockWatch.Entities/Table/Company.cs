using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockWatch.Entities
{
	[Table("Company")]
	public class Company 
	{
		[Key, Column(Order = 0), MaxLength(16)]
		public string Symbol { get; set; }
		[Key, Column(Order = 1), MaxLength(16)]
		public string Exchange { get; set; }
		public string Name { get; set; }
		public float LastSale { get; set; }
		public decimal MarketCap { get; set; }
		public string Sector { get; set; }
		public string Industry { get; set; }

		/*




		public string ToNormalizedLine()
		{
			return string.Format("{0},{1},{2},{3:0.00},{4:0.00},{5},{6}",
				this.Symbol,
				this.Exchange,
				this.Name,
				this.LastSale,
				this.MarketCap,
				this.Sector,
				this.Industry);
		}


		public string Table
		{
			get { return _table; }
		}*/
	}
}

