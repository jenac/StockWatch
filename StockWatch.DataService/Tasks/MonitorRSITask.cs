using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using StockWatch.Internet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockWatch.DataService.Tasks
{
	public class MonitorRSITask : IMonitorTask
	{
		private readonly IMonitorRepository _monitorRepo;

		public MonitorRSITask (IMonitorRepository repo)
		{
			_monitorRepo = repo;
		}

		#region IMonitorTask implementation

		public List<MonitorAlert> Scan ()
		{
			List<Stock> toProcess = _monitorRepo.Stocks.ToList ();
			return toProcess
				.Where (s => s.InPossession || s.SendAlertAfter < DateTime.Now)
				.Select (s => ScanObject (s))
				.Where (n => n != null)
				.ToList ();
		}

		#endregion

		private MonitorAlert ScanObject (Stock monitorObject)
		{
			//get current price
			double curPrice = InternetReader.ReadCurrentPrice (monitorObject.Symbol);
			if (curPrice == -1) {
				return null;
			}
			//load current 30/70 price.
			RSIPredict predict = _monitorRepo.LoadRSIPredict (monitorObject.Symbol);
			if (predict == null) {
				return null;
			}
			if (monitorObject.InPossession && curPrice >= predict.PredictRsi70Price) {
				return new MonitorAlert { 
					Symbol = monitorObject.Symbol,
					Price = curPrice,
					TargetPrice = predict.PredictRsi70Price,
					Buy = false
				};
			}

			if (!monitorObject.InPossession && curPrice <= predict.PredictRsi30Price) {
				return new MonitorAlert {
					Symbol = monitorObject.Symbol,
					Price = curPrice,
					TargetPrice = predict.PredictRsi30Price,
					Buy = true
				};
			}

			return null;
		}
	}
}

