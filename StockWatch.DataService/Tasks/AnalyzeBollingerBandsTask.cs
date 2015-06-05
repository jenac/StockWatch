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
    class AnalyzeBollingerBandsTask: BaseAnalyzeTask
    {
        private readonly IAnalyseRepository _analyseRepo;
        public AnalyzeBollingerBandsTask(IAnalyseRepository repo)
		{
			_analyseRepo = repo;
		}
        public override void Execute()
        {
            List<DataState> toProcess = _analyseRepo.LoadFullIndicatorStateByName(BollingerBands.Name);
            toProcess.ForEach(s => _analyseRepo.SaveIndicator(AnalyzeData(s)));
        }

        public Indicator AnalyzeData(DataState state)
        {
            double[] closePrices = _analyseRepo.LoadClosePriceBySymbol(state.Symbol, true).ToArray();
            List<BollingerBandsData> bBDataValues = BollingerBandsCalculator.CalculateBollingerBands(
                BollingerBandsCalculator.Period,
                BollingerBandsCalculator.DeviationUp,
                BollingerBandsCalculator.DeviationDown,
                closePrices);
            if (bBDataValues.Count == 0) return null;
            var lastBB = AlgorithmHelper.TakeLast<BollingerBandsData>(bBDataValues, 1).FirstOrDefault();
            if (lastBB == null) return null;
            BollingerBands value = new BollingerBands();
            value.Symbol = state.Symbol;
            value.Date = state.Last.Value;
            value.Upper = lastBB.Upper;
            value.Middle = lastBB.Middle;
            value.Lower = lastBB.Lower;
            value.ChannelHight = value.Upper - value.Lower;
            value.ChannelPercent = value.ChannelHight * 100 / value.Middle;
            return value.ToIndicator();
        }
    }
}
