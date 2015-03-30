using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.DataService.Tasks;
using StockWatch.Entities.Complex;
using System;

namespace StockWatch.DataService.Test
{
	[TestClass]
	public class AnalyzeRSIRangeTaskTest
	{
		[TestMethod]
		public void CanAnalyzeRSIRange ()
		{
			var repo = new MocAnalyseRepository ();
			var task = new AnalyzeRSIRangeTask (repo);
			var state = new DataState { Symbol = "AAPL", Last = new DateTime (2012, 4, 4) };
			var result = task.AnalyzeData (state);
			Assert.IsTrue (result != null);
			Assert.IsTrue (result.Symbol == "AAPL");
			Assert.IsTrue (result.Name == RSIRange.Name);
			Assert.IsTrue (result.Date.Year == 2012 && result.Date.Month == 4 && result.Date.Day == 4);
			/*
			<Data>
			  <Min>31.62</Min>
			  <Max>82.39</Max>
			  <L5>39.96</L5>
			  <H5>75.06</H5>
			  <L10>42.93</L10>
			  <H10>74.08</H10>
			  <L15>45.75</L15>
			  <H15>70.41</H15>
			</Data>
			*/
		}
	}
}

