using StockWatch.DataAccess;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockWatch.DataAccess.Repositories
{
	public class MonitorRepository : IMonitorRepository
	{
		private readonly DataContext _context;
		public MonitorRepository (DataContext context)
		{
			_context = context;
		}

		#region IMonitorRepository implementation

		public IEnumerable<Stock> Stocks {
			get {
                return _context.LoadAllStocks();
			}
		}

		public RSIPredict LoadRSIPredict (string symbol)
		{
			return _context.LoadRSIPredict (symbol).FirstOrDefault ();
		}

		#endregion
	}
}

