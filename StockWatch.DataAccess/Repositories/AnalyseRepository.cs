using StockWatch.DataAccess;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System.Collections.Generic;
using System.Linq;

namespace StockWatch.DataAccess.Repositories
{
	public class AnalyseRepository : IAnalyseRepository
	{
		private readonly DataContext _context;
		public AnalyseRepository (DataContext context)
		{
			_context = context;
		}

		#region IAnalyseRepository implementation
		public List<DataState> LoadFullIndicatorStateByName (string name)
		{
			return _context.LoadFullIndicatorState (name);
		}

		public IEnumerable<ComputedEod> LoadComputedEod (string symbol = null)
		{
			return _context.LoadComputedEod (symbol);
		}

		public void SaveIndicator (Indicator value)
		{
			if (value == null) return;
			_context.SaveIndicator (value);
		}

		public IEnumerable<double> LoadClosePriceBySymbol (string symbol, bool sortAsc)
		{
			var eods = _context.Eods.Where (e => e.Symbol == symbol);
			if (sortAsc)
				return eods.OrderBy(e => e.Date).Select (e => e.Close);
			else
				return eods.OrderByDescending (e => e.Date).Select (e => e.Close);
				
		}
		#endregion
	}
}

