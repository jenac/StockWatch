using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.Entities.Table
{
    public class EmailArchive
    {
        public string Name { get; set; }
        public DateTime DateSent { get; set; }
        public string Html { get; set; }
    }
}
