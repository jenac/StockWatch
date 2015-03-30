using StockWatch.DataAccess;
using StockWatch.DataService.Repositories;
using StockWatch.DataService.Tasks;
using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockWatch.DataService.Workers
{
	public class DataWorker : IServiceWorker
	{
		private readonly DataRepository _dataRepo;
		private readonly AnalyseRepository _analyseRepo;
		private readonly List<IDataTask> _tasks;
		private int _count = 0;
		public DataWorker (DataContext context)
		{
			if (context == null)
				throw new ArgumentNullException ();
			_dataRepo = new DataRepository (context);
			_analyseRepo = new AnalyseRepository (context);
			_tasks = new List<IDataTask> {
				new UpdateCompanyTask(_dataRepo),
				new UpdateEodTask(_dataRepo),
				new AnalyzeProfitTask(_analyseRepo),
				new AnalyzeRSITask(_analyseRepo),
				new AnalyzeSMATask(_analyseRepo),
				new AnalyzeRSIPredictTask(_analyseRepo),
				new AnalyzeRSIRangeTask(_analyseRepo),
				new AnalyzeGainLossTask(_analyseRepo)
			};
		}

		#region IServiceWorker implementation

		public void DoWork ()
		{
			Logger.Instance.InfoFormat("Data Worker is on duty: {0}", _count);
			if (_count == 0) {
				foreach (IDataTask task in _tasks) {
					Logger.Instance.InfoFormat ("Executing {0} task.", task.GetType());
					Stopwatch sw = new Stopwatch ();
					sw.Start ();
					task.Execute ();
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
				return !ServiceHelper.InTradingTime(DateTime.Now);
			}
		}


		public int Interval {
			get {
				return 12;
			}
		}
		#endregion
	}
}

