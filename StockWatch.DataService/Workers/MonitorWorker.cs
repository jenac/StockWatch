using StockWatch.DataAccess;
using StockWatch.DataAccess.Repositories;
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
		private readonly List<ITask> _tasks;
		private int _count = 0;
        public MonitorWorker(DataContext context)
		{
			if (context == null)
				throw new ArgumentNullException ();
			_monitorRepo = new MonitorRepository (context);
            _tasks = new List<ITask> {
				new MonitorRSITask(_monitorRepo),
			};
		}
		#region IServiceWorker implementation



		public void DoWork ()
		{
			Logger.Instance.InfoFormat("Monitor Worker is on duty: {0}", _count);
			var totalAlerts = new List<PriceAlert> ();
			if (_count == 0) {
                foreach (ITask task in _tasks)
                {
					Logger.Instance.InfoFormat ("Executing {0} task.", task.GetType());
					Stopwatch sw = new Stopwatch ();
					sw.Start ();
                    task.Execute();
					sw.Stop ();
					Logger.Instance.InfoFormat ("Executing {0} task takes {1:00}:{2:00}:{3:00}", 
						task.GetType(), sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds);
				}
				

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

