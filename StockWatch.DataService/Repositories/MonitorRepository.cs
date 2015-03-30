using StockWatch.DataAccess;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System.Collections.Generic;
using System.Linq;

namespace StockWatch.DataService.Repositories
{
	public class MonitorRepository : IMonitorRepository
	{
		private readonly DataContext _context;
		public MonitorRepository (DataContext context)
		{
			_context = context;
		}

		#region IMonitorRepository implementation
		public IEnumerable<MonitorObject> MonitorObjects {
			get {
				return _context.MonitorObjects;
			}
		}

		public RSIPredict LoadRSIPredict (string symbol)
		{
			return _context.LoadRSIPredict (symbol).FirstOrDefault ();
		}

		#endregion
	}
}

