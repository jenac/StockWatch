using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.DataAccess.Repositories
{
    class SummaryRepository: ISummaryRepository
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
    }
}
