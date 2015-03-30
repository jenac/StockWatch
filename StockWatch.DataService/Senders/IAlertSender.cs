using System;
using StockWatch.Entities;
using System.Collections.Generic;

namespace StockWatch.DataService.Senders
{
	public interface IAlertSender
	{
		void SendAlerts(List<MonitorAlert> alerts);
	}
}

