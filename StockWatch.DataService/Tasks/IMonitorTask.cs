using StockWatch.Entities.Complex;
using System.Collections.Generic;

namespace StockWatch.DataService.Tasks
{
	public interface IMonitorTask
	{
		List<MonitorAlert> Scan ();
	}
}

