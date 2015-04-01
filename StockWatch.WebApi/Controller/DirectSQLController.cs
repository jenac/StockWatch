using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StockWatch.WebApi.Controller
{
    [RoutePrefix("api/directsql")]
    public class DirectSQLController : ApiController
    {
        private StockDataContext _dataCtx;
        public DirectSQLController()
        {
            _dataCtx = new StockDataContext(
                ServiceSettings.Instance.StockDataConnectionString);
        }

        // GET api/directsql/
        [Route]
        public IEnumerable<DirectSQL> Get()
        {
            return _dataCtx.LoadDirectSQL();
        }

        // GET api/directsql/5
        [Route("{name}")]
        public DirectSQL Get(string name)
        {
            return _dataCtx.LoadDirectSQL(name).FirstOrDefault();
        }

        // POST api/directsql
        [Route]
        public void Post(DirectSQL value)
        {
            _dataCtx.SaveResearch(value.ToResearch());
        }

        // PUT api/watch
        [Route("{name}")]
        public void Put(string name, DirectSQL value)
        {
            if (name == value.Name)
            {
                _dataCtx.SaveResearch(value.ToResearch());
            }
        }

        // DELETE api/directsql/5
        [Route("{name}")]
        public void Delete(string name)
        {
            _dataCtx.DeleteResearch(name);
        }

        // GET api/directsql/run/{name}
        [Route("run/{name}")]
        [HttpGet]
        public IEnumerable<ResearchResult> Run(string name)
        {
            DirectSQL sql = _dataCtx.LoadDirectSQL(name).FirstOrDefault();
            if (sql != null)
                return _dataCtx.RunDirectSql(sql.SqlStmt);
            else
                return new List<ResearchResult>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataCtx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
