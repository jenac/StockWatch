using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.Utility;

namespace StockWatch.Internet.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class InternetParserTest
    {
        [TestMethod]
        public void CanParseEod()
        {
            string csv = @"22-Jan-15,110.26,112.47,109.72,112.40,53796409";
            var eod = InternetParser.ParseEod(csv, @"AAA");
            Assert.IsTrue(eod != null);
            Assert.IsTrue(eod.Symbol == "AAA");
            Assert.IsTrue(eod.Date == new DateTime(2015, 1, 22));
            Assert.IsTrue(eod.Open.AlmostEqual(110.26));
            Assert.IsTrue(eod.High.AlmostEqual(112.47));
            Assert.IsTrue(eod.Low.AlmostEqual(109.72));
            Assert.IsTrue(eod.Close.AlmostEqual(112.40));
            Assert.IsTrue(eod.Volume == 53796409);
        }

        [TestMethod]
        public void CanParseCompany()
        {
            string csv = @"""MMM"",""3M Company"",""165.89"",""$106.31B"",""n/a"",""Health Care"",""Medical/Dental Instruments"",""http://www.nasdaq.com/symbol/mmm"",";
            var company = InternetParser.ParseCompany(csv, @"NYSE");
            Assert.IsTrue(company != null);
            Assert.IsTrue(company.Symbol == @"MMM");
            Assert.IsTrue(company.Exchange == @"NYSE");
            Assert.IsTrue(company.Name == @"3M Company");
        }
    }
}
