using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.DataService.Tasks;
using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;
using System.Collections.Generic;
using System.Linq;

namespace StockWatch.DataService.Test
{
	[TestClass]
	public class UpdateCompanyTaskTest
	{
		[TestMethod]
		public void CanGetCompanies ()
		{
			MockDataRepository repo = new MockDataRepository ();
			UpdateCompanyTask task = new UpdateCompanyTask (repo);
			task.Execute ();
			List<string> lines = repo.SelectTable (EntityHelper.GetTableName(typeof(Company)));
			Assert.IsTrue (lines.Count () > 0);
			Assert.IsTrue(lines.Where(l => l.StartsWith("AAPL")).Count() == 0);
		}
	}
}

