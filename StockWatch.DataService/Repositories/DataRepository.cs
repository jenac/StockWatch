using System;
using StockWatch.DataAccess;
using System.Collections.Generic;
using System.Linq;
using StockWatch.Entities;

namespace StockWatch.DataService.Repositories
{
	public class DataRepository : IDataRepository
	{
		private readonly DataContext _context;
		public DataRepository (DataContext context)
		{
			_context = context;
		}


		#region IDataRepository implementation

		public List<DataState> EodState {
			get {
				return _context.LoadEodState();
			}
		}
		public void BulkInsert (string file, string table)
		{
			_context.LoadData (file, table);
		}

		public IEnumerable<string> CompanySymbols {
			get {
				return _context.Companies.Select (c => c.Symbol);
			}
		}
		#endregion
	}
}

