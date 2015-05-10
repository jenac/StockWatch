using StockWatch.Algorithm;
using StockWatch.Algorithm.Schema;
using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using StockWatch.Entities.Table;
using System.Collections.Generic;
using System.Linq;

namespace StockWatch.DataService.Tasks
{
    public class AnalyzeRSIRangeTask : BaseAnalyzeTask
	{
		private readonly IAnalyseRepository _analyseRepo;

		public AnalyzeRSIRangeTask (IAnalyseRepository repo)
		{
			_analyseRepo = repo;
		}

		public override void Execute()
		{
			List<DataState> toProcess = _analyseRepo.LoadFullIndicatorStateByName (RSIRange.Name);
			toProcess.ForEach (s => _analyseRepo.SaveIndicator (AnalyzeData (s)));
		}

		public Indicator AnalyzeData (DataState state)
		{
			List<double> closePrices = _analyseRepo.LoadClosePriceBySymbol (state.Symbol, true).ToList ();

			RSIRangeData range = RSICalculator.GetRSIRange (RSICalculator.Period, closePrices);
			if (range == null)
				return null;
			RSIRange value = new RSIRange ();
			value.Symbol = state.Symbol;
			value.Date = state.Last.Value;
			value.Min = range.Min;
			value.Max = range.Max;
			value.L5 = range.L5;
			value.H5 = range.H5;
			value.L10 = range.L10;
			value.H10 = range.H10;
			value.L15 = range.L15;
			value.H15 = range.H15;
			return value.ToIndicator ();
		
		}


	}
}

