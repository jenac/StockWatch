using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.Entities.Table
{
    //Research use new code first way to mapping to "existing" table
    public class Research
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }
}
