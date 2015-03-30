using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.DataService.Tasks;
using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;
using System.Collections.Generic;
using System.Linq;

namespace StockWatch.DataService.Test
{
	[TestClass]
	public class UpdateEodTaskTest
	{
		[TestMethod]
		public void CanGetEods ()
		{
			MockDataRepository repo = new MockDataRepository ();
			UpdateEodTask task = new UpdateEodTask (repo);
			task.Execute ();
			List<string> lines = repo.SelectTable (EntityHelper.GetTableName(typeof(Eod)));
			Assert.IsTrue (lines.Count () > 0);
			Assert.IsTrue(lines.Where(l => l.StartsWith("AAPL")).Count() > 0);
		}
	}
}

