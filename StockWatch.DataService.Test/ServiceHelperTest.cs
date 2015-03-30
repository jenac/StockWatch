using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StockWatch.DataService.Test
{
	[TestClass]
	public class ServiceHelperTest
	{
		[TestMethod]
		public void TestInTradingTime ()
		{
			DateTime day = new DateTime (2015, 2, 10);
			bool b = ServiceHelper.InTradingTime (day.AddHours(12));
			Assert.IsTrue(b);
			b = ServiceHelper.InTradingTime(day.AddHours(20));
			Assert.IsTrue(!b);
			day = new DateTime (2015, 2, 14);
			b = ServiceHelper.InTradingTime (day);
			Assert.IsTrue(!b);
		}
	}
}

