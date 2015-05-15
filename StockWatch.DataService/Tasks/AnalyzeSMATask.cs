using StockWatch.Algorithm;
using StockWatch.Algorithm.Helper;
using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using StockWatch.Entities.Table;
using System.Collections.Generic;
using System.Linq;


namespace StockWatch.DataService.Tasks
{
    public class AnalyzeSMATask : BaseAnalyzeTask
    {
        private readonly IAnalyseRepository _analyseRepo;

        public AnalyzeSMATask(IAnalyseRepository repo)
        {
            _analyseRepo = repo;
        }

        public override void Execute()
        {
            List<DataState> toProcess = _analyseRepo.LoadFullIndicatorStateByName(SMA.Name);
            toProcess.ForEach(s => _analyseRepo.SaveIndicator(AnalyzeData(s)));
        }

        public Indicator AnalyzeData(DataState state)
        {
            double[] closePrices = _analyseRepo.LoadClosePriceBySymbol(state.Symbol, true).ToArray();

            double[] sma5Values = SMACalculator.CalculateSMA(5, closePrices);
            double[] sma10Values = SMACalculator.CalculateSMA(10, closePrices);
            double[] sma20Values = SMACalculator.CalculateSMA(20, closePrices);
            double[] sma50Values = SMACalculator.CalculateSMA(50, closePrices);
            double[] sma200Values = SMACalculator.CalculateSMA(200, closePrices);

            SMA value = new SMA();
            value.Symbol = state.Symbol;
            value.Date = state.Last.Value;
            value.SMA5 = AlgorithmHelper.GetLast(sma5Values);
            value.SMA10 = AlgorithmHelper.GetLast(sma10Values);
            value.SMA20 = AlgorithmHelper.GetLast(sma20Values);
            value.SMA50 = AlgorithmHelper.GetLast(sma50Values);
            value.SMA200 = AlgorithmHelper.GetLast(sma200Values);

            return value.ToIndicator();

        }
    }
}

