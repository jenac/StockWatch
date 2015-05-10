using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;

namespace StockWatch.DataService.Test
{
	public class MocMonitorRepository : IMonitorRepository
	{
		public MocMonitorRepository ()
		{
		}

		#region IMonitorRepository implementation

		public RSIPredict LoadRSIPredict (string symbol)
		{
			throw new NotImplementedException ();
		}

		public IEnumerable<Stock> Stocks {
			get {
				throw new NotImplementedException ();
			}
		}

		#endregion
	}
}

