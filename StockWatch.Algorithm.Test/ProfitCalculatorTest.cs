using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using StockWatch.Utility;
namespace StockWatch.Algorithm.Test
{
    [TestClass]
    public class ProfitCalculatorTest
    {
        [TestMethod]
        public void CanCalculateProfit()
        {
            double[] raw = new double[] { 1.0, 2.2, 3.1, 6, 7, 2.3, 7.2, 8.2 };
            var profit = ProfitCalculator.CalculateProfit(raw.Length, raw);

            Assert.IsTrue(profit.AlmostEqual(-87.8));
            profit = ProfitCalculator.CalculateProfit(2, raw);
            Assert.IsTrue(profit.AlmostEqual(-54.545454));
        }
    }
}
