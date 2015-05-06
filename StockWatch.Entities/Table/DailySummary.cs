using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.Entities.Table
{
    public class DailySummary
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public string Version { get; set; }
        public string Data { get; set; }
        
    }
}
