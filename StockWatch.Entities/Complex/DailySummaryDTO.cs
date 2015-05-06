using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.Entities.Complex
{
    public abstract class DailySummaryDTO
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public abstract DailySummary ToDailySummary();
    }
}
