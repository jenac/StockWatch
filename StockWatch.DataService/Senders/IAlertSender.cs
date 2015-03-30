using StockWatch.Entities.Complex;
using System.Collections.Generic;

namespace StockWatch.DataService.Senders
{
	public interface IAlertSender
	{
		void SendAlerts(List<MonitorAlert> alerts);
	}
}

