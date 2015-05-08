
namespace StockWatch.Entities.Complex
{
	public class PriceAlert
	{
		public string Symbol { get; set; }
		public double Price { get; set; }
		public double TargetPrice { get; set; }
		public bool Buy { get; set; }
	}
}

