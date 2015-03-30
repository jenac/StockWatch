using System;
using System.Collections.Generic;
using StockWatch.Entities;

namespace StockWatch.DataService.Repositories
{
	public interface IMonitorRepository
	{
		IEnumerable<MonitorObject> MonitorObjects { get; }

		RSIPredict LoadRSIPredict(string symbol);
	}
}

