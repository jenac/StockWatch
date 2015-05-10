using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using StockWatch.Entities.Complex.Summaries;
using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.DataAccess.Repositories
{
    public class SummaryRepository: ISummaryRepository
    {
        private readonly DataContext _context;
        public SummaryRepository(DataContext context)
		{
			_context = context;
		}

        public IEnumerable<DataState> LoadDailySummaryState()
        {
            return _context.LoadDailySummaryState();
        }


        public void SaveDailySummary(DailySummary value)
        {
            _context.SaveDailySummary(value);
        }


        public IEnumerable<Stock> Stocks
        {
            get {
                return _context.LoadAllStocks();
            }
        }


        public Eod GetLastEod(string symbol)
        {
            return _context.GetLastEod(symbol);
        }

        public DailySummary GetLastSummary(string symbol)
        {
            return _context.GetLastSummary(symbol);
        }

        public ADX GetADX(string symbol, DateTime date)
        {
            Indicator value = _context.GetIndicator(symbol, ADX.Name, date);
            if (value == null) return null;
            return EntityHelper.DeserializeFromXml<ADX>(value.Data); 
        }

        public SMA GetSMA(string symbol, DateTime date)
        {
            Indicator value = _context.GetIndicator(symbol, SMA.Name, date);
            if (value == null) return null;
            return EntityHelper.DeserializeFromXml<SMA>(value.Data); 
        }

        public RSI GetRSI(string symbol, DateTime date)
        {
            Indicator value = _context.GetIndicator(symbol, RSI.Name, date);
            if (value == null) return null;
            return EntityHelper.DeserializeFromXml<RSI>(value.Data); 
        }

        public RSIPredict GetRSIPredict(string symbol, DateTime date)
        {
            Indicator value = _context.GetIndicator(symbol, RSIPredict.Name, date);
            if (value == null) return null;
            return EntityHelper.DeserializeFromXml<RSIPredict>(value.Data); 
        }

        //todo:make it generic
        public DailyV001 GetDailySummaryListForDate(string symbol, DateTime date)
        {
            DailySummary value = _context.GetDailySummary(symbol, date);
            if (value == null) return null;
            return EntityHelper.DeserializeFromXml<DailyV001>(value.Data);
        }


        public EmailArchive GetEmailArchiveForDate(string name, DateTime date)
        {
            return _context.GetEmailArchive(name, date);
        }

        public void SaveEmailArchive(EmailArchive value)
        {
            _context.SaveEmailArchive(value);
        }
    }
}
