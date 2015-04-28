using StockWatch.Algorithm;
using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System.Collections.Generic;
using System.Linq;

namespace StockWatch.DataService.Tasks
{
	public class AnalyzeRSITask: IDataTask
	{
		private readonly IAnalyseRepository _analyseRepo;
		public AnalyzeRSITask (IAnalyseRepository repo)
		{
			_analyseRepo = repo;
		}

		#region IServiceTask implementation

		public void Execute ()
		{
			List<DataState> toProcess = _analyseRepo.LoadFullIndicatorStateByName(RSI.Name);
			toProcess.ForEach(s => _analyseRepo.SaveIndicator(AnalyzeData(s)));
		}

		#endregion

		public Indicator AnalyzeData(DataState state)
		{
			double[] closePrices = _analyseRepo.LoadClosePriceBySymbol(state.Symbol, true).ToArray();
			double[] RsiValues = RSICalculator.CalculateRsi(RSICalculator.Period, closePrices);
			if (RsiValues.Length == 0) return null;

			var cont = new ContinuousCalculator(RsiValues, r => r > 50.0);
			RSI value = new RSI();
			value.Symbol = state.Symbol;
			value.Date = state.Last.Value;
			value.Avg = RsiValues.Average();
            value.LastRSI = AlgorithmHelper.GetLast(RsiValues);
			value.PercentGT50 = RsiValues.Where(r => r > 50).Count() * 100 / RsiValues.Count();
			value.TotalDays = RsiValues.Count();
			value.MaxContGT50Days = cont.TrueMax;
			value.MaxContLT50Days = cont.FalseMax;
			value.AvgContGT50Days = cont.TrueAvg;
			value.AvgContLT50Days = cont.FalseAvg;
			value.LastContDays = cont.LastCont;
			return value.ToIndicator();
		}
	}
}

