using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.Entities;
using System.Diagnostics;
using System.Linq;

namespace StockWatch.Internet.Test
{
    /// <summary>
    /// Summary description for InternetReaderTest
    /// </summary>
    [TestClass]
    public class InternetReaderTest
    {
        [TestMethod]
        public void CanWebGet()
        {
            string page = InternetReader.WebGet(@"http://www.google.com/");
            Assert.IsTrue(!string.IsNullOrEmpty(page));
        }

        [TestMethod]
        public void CanReadCompaniesByExchange()
        {
            var companies = InternetReader.ReadCompaniesByExchange(@"NYSE");
            Assert.IsTrue(companies.Count > 0);
        }

        [TestMethod]
        public void CanReadETFAsCompanies()
        {
            var companies = InternetReader.ReadETFAsCompanies();
            Assert.IsTrue(companies.Count > 0);
        }

        [TestMethod]
        public void CanGetLastTradeDate()
        {
            var lastTradeDate = InternetReader.GetLastTradeDate();
            Assert.IsTrue(lastTradeDate.HasValue);
        }

        [TestMethod]
        public void CanReadEodBySymbol()
        {
            EodParam param = new EodParam
            {
                Symbol = "AAPL",
                Start = DateTime.Now.AddDays(-30),
                End = DateTime.Now
            };

            var eods = InternetReader.ReadEodBySymbol(param);
            Assert.IsTrue(eods.Count > 0);
        }

        [TestMethod]
        public void CanReadCurrentPrice()
        {
            double price = InternetReader.ReadCurrentPrice("AAPL");
            Assert.IsTrue(price != 0);
        }

        [TestMethod]
        public void WebGetPerformance()
        {
            var watch = new Stopwatch();
            DateTime start = DateTime.Now.AddDays(-700);
            List<string> symbols = new List<string> {
				"AA", "AAC", "AADR", "AAIT", "AAL", "AAMC", "AAME", "AAN", "A", 
				"AAOI", "AAON", "AAP", "AAT", "AAU", "AAV", "AAVL", "AAWW", "AAXJ", 
				"AB", "ABAC", "ABAX", "ABB", "ABBV", "ABC", "ABCB", "ABCD", "ABCO", 
				"ABCW", "ABDC", "ABEV", "ABG", "ABGB", "ABIO", "ABM", "ABMD", "ABR", 
				"ABT", "ABTL", "ABX", "ABY", "ACAD", "ACAS", "ACAT", "ACC", "ACCO", 
				"ACCU", "ACE", "ACET", "ACFC", "ACFN", "ACG", "ACGL", "ACH", "ACHC", 
				"ACHN", "ACI", "ACIM", "ACIW", "ACLS", "ACM", "ACN", "ACNB", "ACOR", 
				"ACP", "ACPW", "ACRE", "ACRX", "ACSF", "ACST", "ACT", "ACTA", "ACTG", 
				"ACTS", "ACU", "ACUR", "ACW", "ACWI", "ACWV", "ACWX", "ACXM", "ACY", 
				"ADAT", "ADBE", "ADC", "ADEP", "ADGE", "ADHD", "ADI", "ADM", "ADMA", 
				"ADMP", "ADMS", "ADNC", "ADP", "ADPT", "ADRA", "ADRD", "ADRE", "ADRU",
				"AAPL", "GOOG", "MSFT", "XOM", "ORCL"
			};
            var eodParams = symbols.Select(s => new EodParam
            {
                Symbol = s,
                Start = start,
                End = DateTime.Now
            }).ToList();

            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < 10; i++)
            {
                watch.Start();
                foreach (var param in eodParams)
                    InternetReader.ReadEodBySymbol(param);
                watch.Stop();
                times.Add(watch.Elapsed);
                watch.Reset();
            }


        }
    }
}
