using System;

namespace StockWatch.DataService.Workers
{
	public interface IServiceWorker
	{
		bool OnDuty{ get; }
		void DoWork();
		int Interval { get; }
	}
}

