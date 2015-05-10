using StockWatch.Entities.Complex.Indicators;
using StockWatch.Entities.Table;
using System.Collections.Generic;

namespace StockWatch.DataAccess.Repositories
{
	public interface IMonitorRepository
	{
		IEnumerable<Stock> Stocks { get; }

		RSIPredict LoadRSIPredict(string symbol);
	}
}

