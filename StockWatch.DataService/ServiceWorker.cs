using StockWatch.DataAccess;
using StockWatch.DataAccess.Repositories;
using StockWatch.DataService.Tasks;
using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.DataService
{
    class ServiceWorker
    {
        private readonly IDataRepository _dataRepo;
        private readonly IAnalyseRepository _analyseRepo;
        private readonly IMonitorRepository _monitorRepo;
        private readonly ISummaryRepository _summaryRepo;
        private readonly List<ITask> _tasks;
        private int _count = 0;
        public ServiceWorker(DataContext context)
        {
            if (context == null)
                throw new ArgumentNullException();
            _dataRepo = new DataRepository(context);
            _analyseRepo = new AnalyseRepository(context);
            _monitorRepo = new MonitorRepository(context);
            _summaryRepo = new SummaryRepository(context);
            _tasks = new List<ITask> {
                new UpdateCompanyTask(_dataRepo),
				new UpdateEodTask(_dataRepo),
				new AnalyzeProfitTask(_analyseRepo),
				new AnalyzeRSITask(_analyseRepo),
				new AnalyzeSMATask(_analyseRepo),
				new AnalyzeRSIPredictTask(_analyseRepo),
				new AnalyzeRSIRangeTask(_analyseRepo),
				new AnalyzeGainLossTask(_analyseRepo),
                new AnalyzeADXTask(_analyseRepo),
                new AnalyzeMACDTask(_analyseRepo),
                new MonitorRSITask(_monitorRepo),
                new DailySummaryTask(_summaryRepo),
			};
        }

        public void DoWork()
        {
            Logger.Instance.Info("ServiceWorker running tasks...");
            foreach (ITask task in _tasks)
            {
                Logger.Instance.InfoFormat("Task {0} ticking {1}/{2}.", task.GetType(), _count % task.ExecuteInterval, task.ExecuteInterval);
                if (task.TimeToExecute &&
                    (_count % task.ExecuteInterval) == 0)
                {
                    Logger.Instance.InfoFormat("Executing {0} task.", task.GetType());
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    task.Execute();
                    sw.Stop();
                    Logger.Instance.InfoFormat("Executing {0} task takes {1:00}:{2:00}:{3:00}",
                        task.GetType(), sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds);
                }
            }
            _count = _count + 1;
            _count = _count % 288; // 1 day is 5 mins *288
        }
    }
}
