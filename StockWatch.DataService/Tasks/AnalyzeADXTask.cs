using StockWatch.Algorithm;
using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.DataService.Tasks
{
    public class AnalyzeADXTask : BaseAnalyzeTask
    {
        private readonly IAnalyseRepository _analyseRepo;
        public AnalyzeADXTask(IAnalyseRepository repo)
		{
			_analyseRepo = repo;
		}
        public override void Execute()
        {
            List<DataState> toProcess = _analyseRepo.LoadFullIndicatorStateByName(ADX.Name);
            toProcess.ForEach(s => _analyseRepo.SaveIndicator(AnalyzeData(s)));
        }

        public Indicator AnalyzeData(DataState state)
        {
            double[] highPrices = _analyseRepo.LoadHighPriceBySymbol(state.Symbol, true).ToArray();
            double[] lowPrices = _analyseRepo.LoadLowPriceBySymbol(state.Symbol, true).ToArray();
            double[] closePrices = _analyseRepo.LoadClosePriceBySymbol(state.Symbol, true).ToArray();
            double[] adxValues = ADXCalculator.CalculateADX(ADXCalculator.Period, highPrices, lowPrices, closePrices);
            if (adxValues.Length == 0) return null;

            ADX value = new ADX();
            value.Symbol = state.Symbol;
            value.Date = state.Last.Value;
            value.ADX14 = AlgorithmHelper.GetLast(adxValues);
            return value.ToIndicator();
        }
    }
}
