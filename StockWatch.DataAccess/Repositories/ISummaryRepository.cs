using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using StockWatch.Entities.Complex.Summaries;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.DataAccess.Repositories
{
    public interface ISummaryRepository
    {
        IEnumerable<DataState> LoadDailySummaryState();

        IEnumerable<Stock> Stocks { get;  }

        Eod GetLastEod(string symbol);

        DailySummary GetLastSummary(string symbol);

        ADX GetADX(string symbol, DateTime date);

        SMA GetSMA(string symbol, DateTime date);

        RSI GetRSI(string symbol, DateTime date);

        RSIPredict GetRSIPredict(string symbol, DateTime date);

        void SaveDailySummary(DailySummary value);

        DailyV001 GetDailySummaryListForDate(string symbol, DateTime date);

        EmailArchive GetEmailArchiveForDate(string name, DateTime date);

        void SaveEmailArchive(EmailArchive value);
        
    }
}
