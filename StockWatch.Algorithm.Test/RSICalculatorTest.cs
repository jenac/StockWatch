using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using StockWatch.Utility;
namespace StockWatch.Algorithm.Test
{
    [TestClass]
    public class RSICalculatorTest
    {
        private int _period = 14;
        //http://stockcharts.com/school/doku.php?id=chart_school:technical_indicators:relative_strength_index_rsi
        private List<double> _shortPrices = new List<double> {
			44.34, 44.09, 44.15, 43.61, 44.33, 
			44.83, 45.10, 45.42, 45.84, 46.08, 
			45.89, 46.03, 45.61, 46.28, 46.28,
			46.00, 46.03, 46.41, 46.22, 45.64,
			46.21, 46.25, 45.71, 46.45, 45.78,
			45.35, 44.03, 44.18, 44.22, 44.57,
			43.42, 42.66, 43.13};

        [TestMethod]
        public void CanGetFirstAGL()
        {
            AvgGainLossPair value =
                RSICalculator.GetFirstAGL(_period, _shortPrices);
            Assert.IsTrue(value.AvgGain.AlmostEqual(0.24));
            Assert.IsTrue(value.AvgLoss.AlmostEqual(0.10));
        }

        [TestMethod]
        public void CanGetAGL()
        {
            AvgGainLossPair value =
                RSICalculator.GetAGL(_period, 15, _shortPrices);
            Assert.IsTrue(value.AvgGain.AlmostEqual(0.22));
            Assert.IsTrue(value.AvgLoss.AlmostEqual(0.11));

            value = RSICalculator.GetAGL(_period, 32, _shortPrices);
            Assert.IsTrue(value.AvgGain.AlmostEqual(0.18));
            Assert.IsTrue(value.AvgLoss.AlmostEqual(0.30));
        }

        [TestMethod]
        public void CanGetRSI()
        {
            double rs = RSICalculator.GetRS(37.77);
            Assert.IsTrue(rs.AlmostEqual(0.61));
        }



    }
}
