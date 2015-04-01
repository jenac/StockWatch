using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.WebApi.Models
{
    public class SharpChartJS
    {
        public string DailyChartBase64 { get; set; }
        public string WeeklyChartBase64 { get; set; }
    }
}
