using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.Entities.Complex
{
    public abstract class ResearchDTO
    {
        public string Name { get; set; }
        public abstract Research ToResearch();
    }
}
