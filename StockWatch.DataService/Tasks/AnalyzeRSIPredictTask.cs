using StockWatch.Algorithm;
using StockWatch.DataService.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System.Collections.Generic;
using System.Linq;

namespace StockWatch.DataService.Tasks
{
	public class AnalyzeRSIPredictTask : IDataTask
	{
		private readonly IAnalyseRepository _analyseRepo;

		public AnalyzeRSIPredictTask (IAnalyseRepository repo)
		{
			_analyseRepo = repo;
		}

		#region IServiceTask implementation

		public void Execute ()
		{
			List<DataState> toProcess = _analyseRepo.LoadFullIndicatorStateByName (RSIPredict.Name);
			toProcess.ForEach (s => _analyseRepo.SaveIndicator (AnalyzeData (s)));
		}

		#endregion

		public Indicator AnalyzeData (DataState state)
		{
			List<double> closePrices = _analyseRepo.LoadClosePriceBySymbol (state.Symbol, true).ToList ();

			RSIPredict value = new RSIPredict ();
			value.Symbol = state.Symbol;
			value.Date = state.Last.Value;
			value.PredictRsi30Price = RSICalculator.PredictPrice (RSICalculator.Period, 30, closePrices);
			value.PredictRsi50Price = RSICalculator.PredictPrice (RSICalculator.Period, 50, closePrices);
			value.PredictRsi70Price = RSICalculator.PredictPrice (RSICalculator.Period, 70, closePrices);

			return value.ToIndicator ();
		}
	}
}

