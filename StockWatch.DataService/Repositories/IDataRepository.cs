using StockWatch.Entities.Complex;
using System.Collections.Generic;

namespace StockWatch.DataService.Repositories
{
	//The repo interface for data worker.
	//bulk insert for eod and company
	//implement moc repo in unitest
	public interface IDataRepository
	{
		IEnumerable<string> CompanySymbols { get; }

		void BulkInsert (string file, string table);

		List<DataState> EodState { get; }
	}
}

