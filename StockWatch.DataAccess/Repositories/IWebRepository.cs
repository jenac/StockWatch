using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.DataAccess.Repositories
{
    public interface IWebRepository
    {
        IEnumerable<DirectSQL> LoadDirectSQL(string name=null);

        void SaveResearch(Research value);
        
        void DeleteResearch(string name);
    }
}
