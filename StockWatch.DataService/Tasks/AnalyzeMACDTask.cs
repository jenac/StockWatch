using StockWatch.Algorithm;
using StockWatch.Algorithm.Helper;
using StockWatch.Algorithm.Schema;
using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.DataService.Tasks
{
    class AnalyzeMACDTask : BaseAnalyzeTask
    {
        private readonly IAnalyseRepository _analyseRepo;
        public AnalyzeMACDTask(IAnalyseRepository repo)
		{
			_analyseRepo = repo;
		}
        public override void Execute()
        {
            List<DataState> toProcess = _analyseRepo.LoadFullIndicatorStateByName(MACD.Name);
            toProcess.ForEach(s => _analyseRepo.SaveIndicator(AnalyzeData(s)));
        }

        public Indicator AnalyzeData(DataState state)
        {
            double[] closePrices = _analyseRepo.LoadClosePriceBySymbol(state.Symbol, true).ToArray();
            List<MacdData> macdValues = MACDCalculator.CalculateMACD(
                MACDCalculator.FastPeriod,
                MACDCalculator.SlowPeriod,
                MACDCalculator.SignalPeriod,
                closePrices);
            if (macdValues.Count == 0) return null;
            var lastMacd = AlgorithmHelper.TakeLast<MacdData>(macdValues, 1).FirstOrDefault();
            if (lastMacd == null) return null;
            MACD value = new MACD();
            value.Symbol = state.Symbol;
            value.Date = state.Last.Value;
            value.MacdValue = lastMacd.MacdValue;
            value.MacdSingal = lastMacd.MacdSingal;
            value.MacdHIST = lastMacd.MacdHIST;
            return value.ToIndicator();
        }
    }
}
