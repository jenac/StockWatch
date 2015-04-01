using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Complex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StockWatch.DataService.Test
{
	public class MockDataRepository : IDataRepository
	{
		private Dictionary<string, List<string>> _tables;
		public MockDataRepository ()
		{
			_tables = new Dictionary<string, List<string>> ();
		}

		public List<string> SelectTable(string table)
		{
			return _tables [table];
		}



		#region IDataRepository implementation
		public List<DataState> EodState {
			get {
				return new List<DataState> {
					new DataState {
						Symbol = "AAPL",
						Last = null
					},
					new DataState {
						Symbol = "GOOG",
						Last = DateTime.Today.AddDays(-10)
					},
				};
			}
		}

		public void BulkInsert (string file, string table)
		{
			_tables.Add(table, File.ReadLines (file).ToList ());
		}

		public System.Collections.Generic.IEnumerable<string> CompanySymbols {
			get {
				return new List<string> { 
					"AAPL", 
					"GOOG",
				};
			}
		}

		#endregion
	}
}

