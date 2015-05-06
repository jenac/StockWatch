using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;
using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;


namespace StockWatch.DataAccess.Test
{
    [TestClass]
    public class StockDataContextTableTest
    {
        private const string STOCKDATA = "StockData";

        private List<Eod> _eods = new List<Eod> {
			new Eod { Symbol = "A1",
				Date = DateTime.Today,
				Open = 1.0,
				High = 1.1,
				Low = 0.9,
				Close = 1.2,
				Volume = 3000
			}, 
			new Eod { Symbol = "A1",
				Date = DateTime.Today.AddDays (-1),
				Open = 2.0,
				High = 2.1,
				Low = 1.9,
				Close = 2.2,
				Volume = 3002
			}, 
			new Eod { Symbol = "CCC1",
				Date = DateTime.Today,
				Open = 1.0,
				High = 1.1,
				Low = 0.9,
				Close = 1.2,
				Volume = 3000
			}, 
			new Eod { Symbol = "DDDD1",
				Date = DateTime.Today,
				Open = 1.0,
				High = 1.1,
				Low = 0.9,
				Close = 1.2,
				Volume = 3000
			}
		};


        private List<Company> _companies = new List<Company> {
			new Company {
				Symbol = "A",
				Exchange = "A",
				Name = "A Inc.",
				LastSale = 100.0f,
				MarketCap = 200.9M,
				Sector = "A",
				Industry = "A",
			},
			new Company {
				Symbol = "B",
				Exchange = "B",
				Name = "B Inc.",
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
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (var context = new DataContext(STOCKDATA))
            {
                context.Database.ExecuteSqlCommand(
                    string.Format("delete from Eod"));
                context.Database.ExecuteSqlCommand(
                    string.Format("delete from Company"));
            }
        }

        [TestMethod]
        public void CanPersistEod()
        {
            try
            {
                // DbConnection that is already opened
                using (var context = new DataContext(STOCKDATA))
                {
                    // Interception/SQL logging
                    context.Database.Log = (string message) =>
                    {
                        Logger.Instance.Info(message);
                    };
                    context.Eods.AddRange(_eods);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            using (var context = new DataContext(STOCKDATA))
            {
                Assert.IsTrue(context.Eods.Count() > 0);
            }
        }

        [TestMethod]
        public void CanPersistCompany()
        {
            try
            {
                // DbConnection that is already opened
                using (var context = new DataContext(STOCKDATA))
                {
                    // Interception/SQL logging
                    context.Database.Log = (string message) =>
                    {
                        Logger.Instance.Info(message);
                    };
                    // Passing an existing transaction to the context
                    context.Companies.AddRange(_companies);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }


            using (var context = new DataContext(STOCKDATA))
            {
                Assert.IsTrue(context.Companies.Count() > 0);
            }
        }

        [TestMethod]
        public void CanLoadDataforEod()
        {
            string[] lines = {
				//"Symbol,Date,Open,High,Low,Close,Volume",
				"AAPL,2013-12-31,79.17,80.18,79.14,80.15,55819372",
				"AAPL,2013-12-30,79.64,80.01,78.90,79.22,63407722",
				"AAPL,2013-12-29,80.55,80.63,79.93,80.01,56471317",
				"AAPL,2013-12-26,80.55,80.63,79.93,80.01,56471317",
			};
            string file = FileSystem.GetSWTempFolder();
            FileSystem.EnsureFolder(file);
            file = Path.Combine(file, "eod.csv");
            File.WriteAllLines(file, lines);

            int before, after;

            try
            {
                using (var context = new DataContext(STOCKDATA))
                {
                    before = context.Eods.Count();
                    int rows = context.LoadData(file, EntityHelper.GetTableName(typeof(Eod)));
                    after = context.Eods.Count();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                File.Delete(file);
            }
            Assert.IsTrue(after > before);
        }

        [TestMethod]
        public void CanLoadDataforCompany()
        {
            string[] lines = {
				//"Symbol,Exchange,Name,LastSale,MarketCap,Sector,Industry",
				"AAPL,NYSE,Apple Inc.,50,500,IT,IT",
				"GOOG,NASDAQ,Google Inc.,80,800,IT,Search",
				"USO,ETF,American Oil,20,20,Energy,Engergy",
			};
            string file = FileSystem.GetSWTempFolder();
            FileSystem.EnsureFolder(file);
            file = Path.Combine(file, "company.csv");
            File.WriteAllLines(file, lines);

            int before, after;

            try
            {
                using (var context = new DataContext(STOCKDATA))
                {
                    before = context.Companies.Count();
                    int rows = context.LoadData(file, EntityHelper.GetTableName(typeof(Company)));
                    after = context.Companies.Count();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                File.Delete(file);
            }
            Assert.IsTrue(after > before);
        }

    }
}
