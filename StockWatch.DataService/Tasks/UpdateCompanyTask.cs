using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Table;
using StockWatch.Internet;
using StockWatch.Utility;
using System.Collections.Generic;
using System.Linq;

namespace StockWatch.DataService.Tasks
{
	public class UpdateCompanyTask : BaseUpdateTask
	{
		private static readonly List<string> _EXCHANGES = new List<string>
		{
			"NYSE",
			"NASDAQ",
			"AMEX"
		};
		private readonly IDataRepository _dataRepo;
		public UpdateCompanyTask (IDataRepository dataRepo)
		{
			_dataRepo = dataRepo;
		}

		#region IServiceTask implementation

		public override void Execute ()
		{
			List<Company> companies = ReadCompaniesFromInternet();
			List<string> existingCompanySymbols = _dataRepo.CompanySymbols.ToList();
			List<Company> newCompanies = companies
				.Where(c => !existingCompanySymbols.Contains(c.Symbol) 
					&& !c.Symbol.Contains('/')
					&& !c.Symbol.Contains('^')).ToList();
			if (newCompanies.Count > 0)
			{
				Logger.Instance.InfoFormat("Saving {0} new companies.", newCompanies.Count);
				UpdateHelper.BulkSave (_dataRepo, 
					typeof(Company), 
					newCompanies.Select (c => c.ToCsv ()).ToArray ());
			}
			else
			{
				Logger.Instance.Info("Companies up to date.");
			}
		}

		#endregion

		private List<Company> ReadCompaniesFromInternet()
		{
			List<Company> seed = new List<Company> ();
			var companies =
				_EXCHANGES.AsParallel()
					.Select(c => InternetReader.ReadCompaniesByExchange(c))
					.Aggregate(seed, (i, j) => i.Union(j).ToList());
			var etfs = InternetReader.ReadETFAsCompanies();
			companies.AddRange(etfs);
			return companies;
		}
	}
}

