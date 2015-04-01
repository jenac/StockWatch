using StockWatch.Algorithm;
using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System.Collections.Generic;
using System.Linq;


namespace StockWatch.DataService.Tasks
{
	public class AnalyzeSMATask: IDataTask
	{
		private readonly IAnalyseRepository _analyseRepo;

		public AnalyzeSMATask (IAnalyseRepository repo)
		{
			_analyseRepo = repo;
		}

		#region IServiceTask implementation

		public void Execute ()
		{
			List<DataState> toProcess = _analyseRepo.LoadFullIndicatorStateByName (SMA.Name);
			toProcess.ForEach (s => _analyseRepo.SaveIndicator (AnalyzeData (s)));
		}


		#endregion

		public Indicator AnalyzeData (DataState state)
		{
			double[] closePrices = _analyseRepo.LoadClosePriceBySymbol (state.Symbol, true).ToArray ();

			double[] sma50Values = SMACalculator.CalculateSMA (50, closePrices);
			double[] sma200Values = SMACalculator.CalculateSMA (200, closePrices);

			SMA value = new SMA ();
			value.Symbol = state.Symbol;
			value.Date = state.Last.Value;
			value.SMA50 = LastSMA (sma50Values);
			value.SMA200 = LastSMA (sma200Values);

			return value.ToIndicator ();

		}

		private double LastSMA (double[] smaValues)
		{
			if (smaValues.Length == 0)
				return 0;
			return smaValues [smaValues.Length - 1];
		}


	}
}

