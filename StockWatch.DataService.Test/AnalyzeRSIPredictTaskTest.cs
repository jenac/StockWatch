using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.DataService.Tasks;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using System;

namespace StockWatch.DataService.Test
{
	[TestClass]
	public class AnalyzeRSIPredictTaskTest
	{
		[TestMethod]
		public void CanAnalyzeRSIPredict()
		{
			var repo = new MocAnalyseRepository ();
			var task = new AnalyzeRSIPredictTask (repo);
			var state = new DataState { Symbol = "AAPL", Last = new DateTime (2012, 4, 4) };
			var result = task.AnalyzeData (state);
			Assert.IsTrue (result != null);
			Assert.IsTrue (result.Symbol == "AAPL");
			Assert.IsTrue (result.Name == RSIPredict.Name);
			Assert.IsTrue (result.Date.Year == 2012 && result.Date.Month == 4 && result.Date.Day == 4);
			/*
			<Data>
			  <PredictRsi30Price>101.81</PredictRsi30Price>
			  <PredictRsi50Price>120.18</PredictRsi50Price>
			  <PredictRsi70Price>128.06</PredictRsi70Price>
			</Data>
			*/
		}
	}
}

