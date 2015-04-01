using StockWatch.DataAccess;
using StockWatch.DataAccess.Repositories;
using StockWatch.DataService.Senders;
using StockWatch.DataService.Tasks;
using StockWatch.Entities.Complex;
using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockWatch.DataService.Workers
{
	public class MonitorWorker:IServiceWorker
	{
		private readonly MonitorRepository _monitorRepo;
		private readonly IAlertSender _alertSender;
		private readonly List<IMonitorTask> _tasks;
		private int _count = 0;
		public MonitorWorker (DataContext context, IAlertSender sender)
		{
			if (context == null)
				throw new ArgumentNullException ();
			_alertSender = sender;
			_monitorRepo = new MonitorRepository (context);
			_tasks = new List<IMonitorTask> {
				new MonitorRSITask(_monitorRepo),
			};
		}
		#region IServiceWorker implementation



		public void DoWork ()
		{
			Logger.Instance.InfoFormat("Monitor Worker is on duty: {0}", _count);
			var totalAlerts = new List<MonitorAlert> ();
			if (_count == 0) {
				foreach (IMonitorTask task in _tasks) {
					Logger.Instance.InfoFormat ("Executing {0} task.", task.GetType());
					Stopwatch sw = new Stopwatch ();
					sw.Start ();
					var alerts = task.Scan();
					if (alerts != null && alerts.Count > 0)
						totalAlerts.AddRange (alerts);
					sw.Stop ();
					Logger.Instance.InfoFormat ("Executing {0} task takes {1:00}:{2:00}:{3:00}", 
						task.GetType(), sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds);
				}
				Logger.Instance.InfoFormat ("Total {0} alerts.", totalAlerts.Count);
				if (totalAlerts.Count == 0)
					return;
				if (_alertSender != null)
					_alertSender.SendAlerts (totalAlerts);

			}
			_count = _count + 1;
			_count = _count % this.Interval;
		}

		public bool OnDuty {
			get {
				return ServiceHelper.InTradingTime(DateTime.Now);
			}
		}


		public int Interval {
			get {
				return 1;
			}
		}
		#endregion
	}
}

