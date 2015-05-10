using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.DataService.Tasks;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using System;

namespace StockWatch.DataService.Test
{
	[TestClass]
	public class AnalyzeProfitTaskTest
	{
		[TestMethod]
		public void CanAnalyzeProfit()
		{
			var repo = new MocAnalyseRepository ();
			var task = new AnalyzeProfitTask (repo);
			var state = new DataState { Symbol = "AAPL", Last = new DateTime (2012, 4, 4) };
			var result = task.AnalyzeData (state);
			Assert.IsTrue (result != null);
			Assert.IsTrue (result.Symbol == "AAPL");
			Assert.IsTrue (result.Name == Profit.Name);
			Assert.IsTrue (result.Date.Year == 2012 && result.Date.Month == 4 && result.Date.Day == 4);
			/*
		    <Data>
			  <R20Day>14.62</R20Day>
			  <R50Day>13.48</R50Day>
			  <R100Day>29.36</R100Day>
			  <R150Day>37.14</R150Day>
			  <R200Day>53.04</R200Day>
			</Data>
			*/
		}
	}
}

