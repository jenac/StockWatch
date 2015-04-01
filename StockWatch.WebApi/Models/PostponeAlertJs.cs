using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.WebApi.Models
{
    public class PostponeAlertJs
    {
        public string Symbol { get; set; }
        public int PostponseMinutes { get; set; }
    }
}
