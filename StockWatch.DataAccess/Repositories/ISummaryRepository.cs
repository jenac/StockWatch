using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.DataAccess.Repositories
{
    interface ISummaryRepository
    {
        IEnumerable<DataState> LoadDailySummaryState();

        void SaveDailySummary(DailySummary value);
    }
}
