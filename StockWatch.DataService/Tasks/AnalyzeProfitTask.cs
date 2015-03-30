using System;
using StockWatch.Entities;
using System.Collections.Generic;
using StockWatch.Algorithm;
using System.Linq;
using StockWatch.DataService.Repositories;

namespace StockWatch.DataService.Tasks
{
	public class AnalyzeProfitTask : IDataTask
	{
		private readonly IAnalyseRepository _analyseRepo;
		public AnalyzeProfitTask (IAnalyseRepository repo)
		{
			_analyseRepo = repo;
		}

		#region IServiceTask implementation

		public void Execute ()
		{
			List<DataState> toAnalyze = _analyseRepo.LoadFullIndicatorStateByName (Profit.Name);
			toAnalyze.ForEach(s => _analyseRepo.SaveIndicator(AnalyzeData(s)));
		}

		#endregion

		public Indicator AnalyzeData(DataState state)
		{
			double[] closePrices = _analyseRepo.LoadClosePriceBySymbol(state.Symbol, false).ToArray();
			Profit value = new Profit();
			value.Symbol = state.Symbol;
			value.Date = state.Last.Value;
			value.R20Day = ProfitCalculator.CalculateProfit(20, closePrices);
			value.R50Day = ProfitCalculator.CalculateProfit(50, closePrices);
			value.R100Day = ProfitCalculator.CalculateProfit(100, closePrices);
			value.R150Day = ProfitCalculator.CalculateProfit(150, closePrices);
			value.R200Day = ProfitCalculator.CalculateProfit(200, closePrices);
			return value.ToIndicator();
		}


	}
}

