using System;
using StockWatch.Entities;
using System.Collections.Generic;

namespace StockWatch.DataService.Tasks
{
	public interface IMonitorTask
	{
		List<MonitorAlert> Scan ();
	}
}

