using StockWatch.DataAccess.Contexts;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockWatch.Utility;

namespace StockWatch.DataAccess.Repositories
{
    public class WebApiRepository : IWebRepository
    {
        private readonly WebDbContext _context;
        public WebApiRepository(WebDbContext context)
        {
            this._context = context;
        }
        public IEnumerable<DirectSQL> LoadDirectSQL(string name = null)
        {
            List<Research> researches = 
                (string.IsNullOrEmpty(name)) ?
                _context.Researches.Where(r => r.Type == DirectSQL.Type).ToList() :
                _context.Researches.Where(r => r.Type == DirectSQL.Type && r.Name == name).ToList() ;
            return researches.Select(r => new DirectSQL { 
                Name = r.Name, 
                SqlStmt = TextParser.Base64Decode(r.Data) });
        }

        public void SaveResearch(Research value)
        {
            var found = _context.Researches.Where(r => r.Name == value.Name).FirstOrDefault();
            if (found != null)
                found.Data = value.Data;
            else
                _context.Researches.Add(value);
        }

        public void DeleteResearch(string name)
        {
            _context.Researches.Remove(new Research { Name = name });
        }
    }
}
