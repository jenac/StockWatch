using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StockWatch.Algorithm.Test
{
    [TestClass]
    public class ContinuousCalculatorTest
    {
        [TestMethod]
        public void CanDoContinuousCalculation()
        {
            double[] raw = new double[] { 1.0, 2.2, 3.1, 6, 7, 2.3, 7.2, 8.2 };
            var calc = new ContinuousCalculator(
                raw, i => i > 5);

            Assert.IsTrue(calc.FalseMax == 3);
            Assert.IsTrue(calc.FalseAvg == 2.0);
            Assert.IsTrue(calc.TrueMax == 2);
            Assert.IsTrue(calc.TrueAvg == 2.0);
            Assert.IsTrue(calc.LastCont == 2);

            calc = new ContinuousCalculator(
                raw, i => i > 50);
            Assert.IsTrue(calc.FalseMax == 8);
            Assert.IsTrue(calc.FalseAvg == 8.0);
            Assert.IsTrue(calc.TrueMax == 0);
            Assert.IsTrue(calc.TrueAvg == 0.0);
            Assert.IsTrue(calc.LastCont == 8);

            calc = new ContinuousCalculator(
                raw, i => i < 50);
            Assert.IsTrue(calc.FalseMax == 0);
            Assert.IsTrue(calc.FalseAvg == 0.0);
            Assert.IsTrue(calc.TrueMax == 8);
            Assert.IsTrue(calc.TrueAvg == 8.0);
            Assert.IsTrue(calc.LastCont == 8);
        }
    }
}
