using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System.Collections.Generic;

namespace StockWatch.DataAccess.Repositories
{
	public interface IAnalyseRepository
	{
		List<DataState> LoadFullIndicatorStateByName(string name);

		IEnumerable<ComputedEod> LoadComputedEod (string symbol = null);

		void SaveIndicator (Indicator value);

		IEnumerable<double> LoadClosePriceBySymbol (string symbol, bool sortAsc);

	}
}

