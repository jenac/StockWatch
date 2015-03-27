using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using StockWatch.Utility;
namespace StockWatch.Algorithm.Test
{
    [TestClass]
    public class SMACalculatorTest
    {
        [TestMethod]
        public void CanCalculateSMA()
        {
            List<double> _shortPrices = new List<double> {
				11,12,13,14,15,16,17};

            double[] smas = SMACalculator.CalculateSMA(5, _shortPrices.ToArray());
            Assert.IsTrue(smas.Length == 3);
            Assert.IsTrue(smas[0].AlmostEqual(13.0));
            Assert.IsTrue(smas[1].AlmostEqual(14));
            Assert.IsTrue(smas[2].AlmostEqual(15));
        }
    }
}
