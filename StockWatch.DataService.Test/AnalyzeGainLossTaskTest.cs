using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.DataService.Tasks;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using System;

namespace StockWatch.DataService.Test
{
	[TestClass]
	public class AnalyzeGainLossTaskTest
	{
		[TestMethod]
		public void CanAnalyzeGainLoss()
		{
			var repo = new MocAnalyseRepository ();
			var task = new AnalyzeGainLossTask (repo);
			var state = new DataState { Symbol = "AAPL", Last = new DateTime (2012, 4, 4) };
			var result = task.AnalyzeData (state);
			Assert.IsTrue (result != null);
			Assert.IsTrue (result.Symbol == "AAPL");
			Assert.IsTrue (result.Name == GainLoss.Name);
			Assert.IsTrue (result.Date.Year == 2012 && result.Date.Month == 4 && result.Date.Day == 4);
			/*
			<Data>
			<MaxContGainDays>5</MaxContGainDays>
			<AvgContGainDays>2.33</AvgContGainDays>
			<MaxContLossDays>3</MaxContLossDays>
			<AvgContLossDays>1.22</AvgContLossDays>
			<LastGLContDays>1</LastGLContDays>
			</Data>*/

		}
	}
}

