using System;
using StockWatch.Entities;
using System.Collections.Generic;

namespace StockWatch.DataService.Repositories
{
	public interface IAnalyseRepository
	{
		List<DataState> LoadFullIndicatorStateByName(string name);

		IEnumerable<ComputedEod> LoadComputedEod (string symbol = null);

		void SaveIndicator (Indicator value);

		IEnumerable<double> LoadClosePriceBySymbol (string symbol, bool sortAsc);

	}
}

