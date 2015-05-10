using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;
using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StockWatch.DataAccess.Test
{
    [TestClass]
    public class StockDataContextComplexTest
    {
        private const string STOCKDATA = "StockData";

        private List<Eod> _eods = new List<Eod> {
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 19), Open=209.41, High=210.42, Low=209.24, Close=209.98},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 18), Open=209.66, High=210.22, Low=209.34, Close=210.13},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 17), Open=209.40, High=210.32, Low=209.10, Close=210.11},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 13), Open=209.07, High=209.84, Low=208.76, Close=209.78},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 12), Open=207.89, High=208.99, Low=206.97, Close=208.92},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 11), Open=206.61, High=207.45, Low=205.83, Close=206.93},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 10), Open=205.88, High=207.12, Low=204.68, Close=206.81},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 9), Open=204.77, High=205.64, Low=204.14, Close=204.63},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 6), Open=206.56, High=207.24, Low=204.92, Close=205.55},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 5), Open=204.86, High=206.30, Low=204.77, Close=206.12},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 4), Open=203.92, High=205.38, Low=203.51, Close=204.06},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 3), Open=203.00, High=204.85, Low=202.55, Close=204.84},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 2, 2), Open=200.05, High=202.03, Low=197.86, Close=201.92},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 30), Open=200.57, High=202.17, Low=199.13, Close=199.45},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 29), Open=200.38, High=202.30, Low=198.68, Close=201.99},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 28), Open=204.17, High=204.29, Low=199.91, Close=200.14},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 27), Open=202.97, High=204.12, Low=201.74, Close=202.74},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 26), Open=204.71, High=205.56, Low=203.85, Close=205.45},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 23), Open=205.79, High=206.10, Low=204.81, Close=204.97},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 22), Open=203.99, High=206.26, Low=202.33, Close=206.10},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 21), Open=201.50, High=203.66, Low=200.94, Close=203.08},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 20), Open=202.40, High=202.72, Low=200.17, Close=202.06},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 16), Open=198.77, High=201.82, Low=198.55, Close=201.63},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 15), Open=201.63, High=202.01, Low=198.88, Close=199.02},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 14), Open=199.65, High=201.10, Low=198.57, Close=200.86},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 13), Open=204.12, High=205.48, Low=200.51, Close=202.08},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 12), Open=204.41, High=204.60, Low=201.92, Close=202.65},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 9), Open=206.40, High=206.42, Low=203.51, Close=204.25},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 8), Open=204.01, High=206.16, Low=203.99, Close=205.90},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 7), Open=201.42, High=202.72, Low=200.88, Close=202.31},
			new Eod { Symbol="SPY", Date=new DateTime(2015, 1, 6), Open=202.09, High=202.72, Low=198.86, Close=199.82},			

			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 19), Open=128.48, High=129.03, Low=128.33, Close=128.45 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 18), Open=127.62, High=128.78, Low=127.45, Close=128.72 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 17), Open=127.49, High=128.88, Low=126.92, Close=127.83 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 13), Open=127.28, High=127.28, Low=125.65, Close=127.08 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 12), Open=126.06, High=127.48, Low=125.57, Close=126.46 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 11), Open=122.77, High=124.92, Low=122.50, Close=124.88 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 10), Open=120.17, High=122.15, Low=120.16, Close=122.02 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 9), Open=118.55, High=119.84, Low=118.43, Close=119.72 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 6), Open=120.02, High=120.25, Low=118.45, Close=118.93 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 5), Open=120.02, High=120.23, Low=119.25, Close=119.94 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 4), Open=118.50, High=120.51, Low=118.31, Close=119.56 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 3), Open=118.50, High=119.09, Low=117.61, Close=118.65 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 2, 2), Open=118.05, High=119.17, Low=116.08, Close=118.63 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 30), Open=118.40, High=120.00, Low=116.85, Close=117.16 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 29), Open=116.32, High=119.19, Low=115.56, Close=118.90 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 28), Open=117.62, High=118.12, Low=115.31, Close=115.31 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 27), Open=112.42, High=112.48, Low=109.03, Close=109.14 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 26), Open=113.74, High=114.36, Low=112.80, Close=113.10 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 23), Open=112.30, High=113.75, Low=111.53, Close=112.98 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 22), Open=110.26, High=112.47, Low=109.72, Close=112.40 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 21), Open=108.95, High=111.06, Low=108.27, Close=109.55 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 20), Open=107.84, High=108.97, Low=106.50, Close=108.72 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 16), Open=107.03, High=107.58, Low=105.20, Close=105.99 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 15), Open=110.00, High=110.06, Low=106.66, Close=106.82 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 14), Open=109.04, High=110.49, Low=108.50, Close=109.80 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 13), Open=111.43, High=112.80, Low=108.91, Close=110.22 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 12), Open=112.60, High=112.63, Low=108.80, Close=109.25 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 9), Open=112.67, High=113.25, Low=110.21, Close=112.01 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 8), Open=109.23, High=112.15, Low=108.70, Close=111.89 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 7), Open=107.20, High=108.20, Low=106.70, Close=107.75 }, 
			new Eod { Symbol="AAPL", Date=new DateTime(2015, 1, 6), Open=106.54, High=107.43, Low=104.63, Close=106.26 }, 
		};


        private List<Company> _companies = new List<Company> {
			new Company {
				Symbol = "AAPL",
				Exchange = "NYSE",
				Name = "Apple Inc.",
				LastSale = 100.0f,
				MarketCap = 200.9M,
				Sector = "A",
				Industry = "A",
			},
			new Company {
				Symbol = "SPY",
				Exchange = "ETF",
				Name = "SPY",
				LastSale = 102.2f,
				MarketCap = 202.9M,
				Sector = "B",
				Industry = "B",
			},


		};

        [TestInitialize]
        public void Init()
        {
            DataContextInit.RegisterContextInitializers();
            
            using (var context = new DataContext(STOCKDATA))
            {
                context.Database.Initialize(true);//.CreateIfNotExists ();
            }
            
            using (var context = new DataContext(STOCKDATA))
            {
                // Interception/SQL logging
                context.Database.Log = (string message) =>
                {
                    Logger.Instance.Info(message);
                };
                context.Eods.AddRange(_eods);
                context.Companies.AddRange(_companies);
                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (var context = new DataContext(STOCKDATA))
            {
                context.Database.ExecuteSqlCommand(
                    string.Format("delete from {0}", EntityHelper.GetTableName(typeof(Eod))));
                context.Database.ExecuteSqlCommand(
                    string.Format("delete from {0}", EntityHelper.GetTableName(typeof(Company))));
            }
        }

        [TestMethod]
        public void CanLoadEodState()
        {
            List<DataState> states;
            try
            {
                using (var context = new DataContext(STOCKDATA))
                {
                    states = context.LoadEodState();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            Assert.IsTrue(states.Count() > 0);
        }

        [TestMethod]
        public void CanLoadFullIndicatorState()
        {
            List<DataState> states;
            try
            {
                using (var context = new DataContext(STOCKDATA))
                {
                    states = context.LoadFullIndicatorState(Profit.Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            Assert.IsTrue(states.Count() > 0);
        }

        [TestMethod]
        public void CanLoadComputedEod()
        {
            List<ComputedEod> computed;
            try
            {
                using (var context = new DataContext(STOCKDATA))
                {
                    computed = context.LoadComputedEod().ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            Assert.IsTrue(computed.Count() > 0);
        }

        [TestMethod]
        public void CanSaveIndicator()
        {
            List<IndicatorDTO> indicators = new List<IndicatorDTO> {
				new GainLoss {
					Symbol = "SPY",
					Date = DateTime.Today,
					MaxContGainDays = 5,
					AvgContGainDays = 3.2,
					MaxContLossDays = 4, 
					AvgContLossDays = 3.1,
					LastGLContDays = 2,
				},
				new Profit { 
					Symbol = "SPY", 
					Date = DateTime.Today,
					R20Day = 20.0, 
					R50Day = 50.0,
					R100Day = 100.0, 
					R150Day = 150.0,
					R200Day = 200.0
				},
				new RSI {
					Symbol = "SPY",
					Date = DateTime.Today,
					PercentGT50 = 60.0,
					Avg = 58.1, 
					LastRSI = 62.2,
					TotalDays = 700,
					MaxContGT50Days = 40, 
					AvgContGT50Days = 29.1,
					MaxContLT50Days = 30, 
					AvgContLT50Days = 27.2,
					LastContDays = 9
				},
				new RSIPredict {
					Symbol = "SPY",
					Date = DateTime.Today,
					PredictRsi30Price = 30.0, 
					PredictRsi50Price = 50.0, 
					PredictRsi70Price = 70.0
				},
				new RSIRange {
					Symbol = "SPY",
					Date = DateTime.Today,
					Min = 20.0,
					Max = 80.0, 
					L5 = 25.0, 
					H5 = 75.0, 
					L10 = 30.0,
					H10 = 70.0,
					L15 = 35.0, 
					H15 = 65.0
				},
				new SMA {
					Symbol = "SPY",
					Date = DateTime.Today,
                    SMA5 = 5,
                    SMA10 = 10,
                    SMA20 = 20,
					SMA50 = 50, 
					SMA200 = 200
				}, 
			};

            try
            {
                using (var context = new DataContext(STOCKDATA))
                {
                    foreach (IndicatorDTO i in indicators)
                    {
                        context.SaveIndicator(i.ToIndicator());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
