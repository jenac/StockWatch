using StockWatch.Algorithm;
using StockWatch.DataService.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System.Collections.Generic;
using System.Linq;

namespace StockWatch.DataService.Tasks
{
	public class AnalyzeGainLossTask : IDataTask
	{
		private readonly IAnalyseRepository _analyseRepo;
		public AnalyzeGainLossTask (IAnalyseRepository repo)
		{
			_analyseRepo = repo;
		}

		#region IServiceTask implementation

		public void Execute ()
		{
			List<DataState> toAnalyze = _analyseRepo.LoadFullIndicatorStateByName (GainLoss.Name);
			toAnalyze.ForEach(s => _analyseRepo.SaveIndicator(AnalyzeData(s)));
		}

		#endregion


        public Indicator AnalyzeData(DataState state)
        {
        	double[] gainLoss = _analyseRepo.LoadComputedEod(state.Symbol)
                .OrderByDescending(ce=>ce.Date)
                .Select(e=>e.GL).ToArray();
			var calculator = new ContinuousCalculator(gainLoss, gl => gl > 0);
            GainLoss value = new GainLoss();
            value.Symbol = state.Symbol;
            value.Date = state.Last.Value;
            value.MaxContGainDays = calculator.TrueMax;
            value.AvgContGainDays = calculator.TrueAvg;
            value.MaxContLossDays = calculator.FalseMax;
            value.AvgContLossDays = calculator.FalseAvg;
            value.LastGLContDays = calculator.LastCont;
            return value.ToIndicator();
        }
	}
}

