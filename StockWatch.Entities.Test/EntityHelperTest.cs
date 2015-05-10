using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.Entities.Complex.Indicators;
using StockWatch.Entities.Complex.Summaries;
using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;
using System;

namespace StockWatch.Entities.Test
{
    [TestClass]
    public class EntityHelperTest
    {
        [TestMethod]
        public void CanGetTableName()
        {
            Assert.IsTrue(EntityHelper.GetTableName(typeof(Eod)) == "Eod");
            Assert.IsTrue(EntityHelper.GetTableName(typeof(Company)) == "Company");
        }

        [TestMethod]
        public void CanSerializeToXml_ADX()
        {
            var source = new ADX
            {
                Symbol = "A",
                Date = DateTime.Today,
                ADX14 = 12.0
            };

            var i = source.ToIndicator();

            var target = EntityHelper.DeserializeFromXml<ADX> (i.Data);

            Assert.IsTrue(source.Symbol == target.Symbol);
            Assert.IsTrue(source.Date == target.Date);
            Assert.IsTrue(source.ADX14 == target.ADX14);
        }

        [TestMethod]
        public void CanSerializeToXml_GainLoss()
        {
            var source = new GainLoss
            {
                Symbol = "A",
                Date = DateTime.Today,
                MaxContGainDays = 1,
                AvgContGainDays = 2,
                MaxContLossDays = 3,
                AvgContLossDays = 4,
                LastGLContDays = 5,
            };

            var i = source.ToIndicator();

            var target = EntityHelper.DeserializeFromXml<GainLoss>(i.Data);

            Assert.IsTrue(source.Symbol == target.Symbol);
            Assert.IsTrue(source.Date == target.Date);
            Assert.IsTrue(source.MaxContGainDays == target.MaxContGainDays);
            Assert.IsTrue(source.AvgContGainDays == target.AvgContGainDays);
            Assert.IsTrue(source.MaxContLossDays == target.MaxContLossDays);
            Assert.IsTrue(source.AvgContLossDays == target.AvgContLossDays);
            Assert.IsTrue(source.LastGLContDays == target.LastGLContDays);
        }

        [TestMethod]
        public void CanSerializeToXml_Profit()
        {
            var source = new Profit
            {
                Symbol = "A",
                Date = DateTime.Today,
                R20Day = 1.0,
                R50Day = 2.0,
                R100Day = 3.0,
                R150Day = 4.0,
                R200Day = 5.0,
            };

            var i = source.ToIndicator();

            var target = EntityHelper.DeserializeFromXml<Profit>(i.Data);

            Assert.IsTrue(source.Symbol == target.Symbol);
            Assert.IsTrue(source.Date == target.Date);
            Assert.IsTrue(source.R20Day == target.R20Day);
            Assert.IsTrue(source.R50Day == target.R50Day);
            Assert.IsTrue(source.R100Day == target.R100Day);
            Assert.IsTrue(source.R150Day == target.R150Day);
            Assert.IsTrue(source.R200Day == target.R200Day);
        }

        [TestMethod]
        public void CanSerializeToXml_RSI()
        {
            var source = new RSI
            {
                Symbol = "A",
                Date = DateTime.Today,
                PercentGT50 = 1.0,
                Avg = 2.0,
                LastRSI = 3.0,
                TotalDays = 4,
                MaxContGT50Days = 5,
                AvgContGT50Days = 6,
                MaxContLT50Days = 7,
                AvgContLT50Days = 8,
                LastContDays = 9,
            };

            var i = source.ToIndicator();

            var target = EntityHelper.DeserializeFromXml<RSI>(i.Data);

            Assert.IsTrue(source.Symbol == target.Symbol);
            Assert.IsTrue(source.Date == target.Date);
            Assert.IsTrue(source.PercentGT50 == target.PercentGT50);
            Assert.IsTrue(source.Avg == target.Avg);
            Assert.IsTrue(source.LastRSI == target.LastRSI);
            Assert.IsTrue(source.TotalDays == target.TotalDays);
            Assert.IsTrue(source.MaxContGT50Days == target.MaxContGT50Days);
            Assert.IsTrue(source.AvgContGT50Days == target.AvgContGT50Days);
            Assert.IsTrue(source.MaxContLT50Days == target.MaxContLT50Days);
            Assert.IsTrue(source.AvgContLT50Days == target.AvgContLT50Days);
            Assert.IsTrue(source.LastContDays == target.LastContDays);
        }

        [TestMethod]
        public void CanSerializeToXml_RSIPredict()
        {
            var source = new RSIPredict
            {
                Symbol = "A",
                Date = DateTime.Today,
                PredictRsi30Price = 1.0,
                PredictRsi50Price = 2.0,
                PredictRsi70Price = 3.0,
            };

            var i = source.ToIndicator();

            var target = EntityHelper.DeserializeFromXml<RSIPredict>(i.Data);

            Assert.IsTrue(source.Symbol == target.Symbol);
            Assert.IsTrue(source.Date == target.Date);
            Assert.IsTrue(source.PredictRsi30Price == target.PredictRsi30Price);
            Assert.IsTrue(source.PredictRsi50Price == target.PredictRsi50Price);
            Assert.IsTrue(source.PredictRsi70Price == target.PredictRsi70Price);
        }

        [TestMethod]
        public void CanSerializeToXml_RSIRange()
        {
            var source = new RSIRange
            {
                Symbol = "A",
                Date = DateTime.Today,
                Min = 1.0,
                Max = 2.0,
                L5 = 3.0,
                H5 = 4.0,
                L10 = 5.0,
                H10 = 6.0,
                L15 = 7.0,
                H15 = 8.0,
            };

            var i = source.ToIndicator();

            var target = EntityHelper.DeserializeFromXml<RSIRange>(i.Data);

            Assert.IsTrue(source.Symbol == target.Symbol);
            Assert.IsTrue(source.Date == target.Date);
            Assert.IsTrue(source.Min == target.Min);
            Assert.IsTrue(source.Max == target.Max);
            Assert.IsTrue(source.L5 == target.L5);
            Assert.IsTrue(source.H5 == target.H5);
            Assert.IsTrue(source.L10 == target.L10);
            Assert.IsTrue(source.H10 == target.H10);
            Assert.IsTrue(source.L15 == target.L15);
            Assert.IsTrue(source.H15 == target.H15);
        }

        [TestMethod]
        public void CanSerializeToXml_SMA()
        {
            var source = new SMA
            {
                Symbol = "A",
                Date = DateTime.Today,
                SMA5 = 1.0,
                SMA10 = 2.0,
                SMA20 = 3.0,
                SMA50 = 4.0,
                SMA200 = 5.0,

            };

            var i = source.ToIndicator();

            var target = EntityHelper.DeserializeFromXml<SMA>(i.Data);

            Assert.IsTrue(source.Symbol == target.Symbol);
            Assert.IsTrue(source.Date == target.Date);
            Assert.IsTrue(source.SMA5 == target.SMA5);
            Assert.IsTrue(source.SMA10 == target.SMA10);
            Assert.IsTrue(source.SMA20 == target.SMA20);
            Assert.IsTrue(source.SMA50 == target.SMA50);
            Assert.IsTrue(source.SMA200 == target.SMA200);
        }

        [TestMethod]
        public void CanSerializeToXml_DailyV001()
        {
            var source = new DailyV001
            {
                Symbol = "A",
                Date = DateTime.Today,
                ADX14 = 1.0,
                SMAShortTerm = 2.0,
                SMAMidTerm = 3.0,
                SMALongTerm = 4.0,
                RSI14 = 5.0,
                R30Price = 6.0,
                R70Price = 7.0,
                VolumePercentAgainstAvg = 8.0,
            };
            
            var i = source.ToDailySummary();

            var target = EntityHelper.DeserializeFromXml<DailyV001>(i.Data);

            Assert.IsTrue(source.Symbol == target.Symbol);
            Assert.IsTrue(source.Date == target.Date);
            Assert.IsTrue(source.ADX14 == target.ADX14);
            Assert.IsTrue(source.SMAShortTerm == target.SMAShortTerm);
            Assert.IsTrue(source.SMAMidTerm == target.SMAMidTerm);
            Assert.IsTrue(source.SMALongTerm == target.SMALongTerm);
            Assert.IsTrue(source.RSI14 == target.RSI14);
            Assert.IsTrue(source.R30Price == target.R30Price);
            Assert.IsTrue(source.R70Price == target.R70Price);
            Assert.IsTrue(source.VolumePercentAgainstAvg == target.VolumePercentAgainstAvg);
        }
    }
}
