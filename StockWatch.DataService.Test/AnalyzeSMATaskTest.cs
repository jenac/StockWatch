using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.DataService.Tasks;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using System;

namespace StockWatch.DataService.Test
{
	[TestClass]
	public class AnalyzeSMATaskTest
	{
		[TestMethod]
		public void CanAnalyzeSMA ()
		{
			var repo = new MocAnalyseRepository ();
			var task = new AnalyzeSMATask (repo);
			var state = new DataState { Symbol = "AAPL", Last = new DateTime (2012, 4, 4) };
			var result = task.AnalyzeData (state);
			Assert.IsTrue (result != null);
			Assert.IsTrue (result.Symbol == "AAPL");
			Assert.IsTrue (result.Name == SMA.Name);
			Assert.IsTrue (result.Date.Year == 2012 && result.Date.Month == 4 && result.Date.Day == 4);
			/*
			<Data>
			  <SMA50>114.55</SMA50>
			  <SMA200>102.95</SMA200>
			</Data>
			*/
		}
	}
}

