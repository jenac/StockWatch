using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.DataService.Tasks;
using StockWatch.Entities.Complex;
using System;

namespace StockWatch.DataService.Test
{
	[TestClass]
	public class AnalyzeRSITaskTest
	{
		[TestMethod]
		public void CanAnalyzeRSI ()
		{
			var repo = new MocAnalyseRepository ();
			var task = new AnalyzeRSITask (repo);
			var state = new DataState { Symbol = "AAPL", Last = new DateTime (2012, 4, 4) };
			var result = task.AnalyzeData (state);
			Assert.IsTrue (result != null);
			Assert.IsTrue (result.Symbol == "AAPL");
			Assert.IsTrue (result.Name == RSI.Name);
			Assert.IsTrue (result.Date.Year == 2012 && result.Date.Month == 4 && result.Date.Day == 4);
			/*
			<Data>
			  <PercentGT50>73.00</PercentGT50>
			  <Avg>57.71</Avg>
			  <LastRSI>75.53</LastRSI>
			  <TotalDays>352</TotalDays>
			  <MaxContGT50Days>72</MaxContGT50Days>
			  <AvgContGT50Days>10.28</AvgContGT50Days>
			  <MaxContLT50Days>11</MaxContLT50Days>
			  <AvgContLT50Days>3.80</AvgContLT50Days>
			  <LastContDays>17</LastContDays>
			</Data>
			*/
		}
	}
}

