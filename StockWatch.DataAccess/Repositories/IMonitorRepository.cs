using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System.Collections.Generic;

namespace StockWatch.DataAccess.Repositories
{
	public interface IMonitorRepository
	{
		IEnumerable<MonitorObject> MonitorObjects { get; }

		RSIPredict LoadRSIPredict(string symbol);
	}
}

