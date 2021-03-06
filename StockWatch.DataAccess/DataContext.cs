﻿using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Indicators;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
namespace StockWatch.DataAccess
{
	public class DataContext : DbContext
	{
		protected ObjectContext _objectCtx;
		//Raw Data
		public DbSet<Eod> Eods { get; set; }
		public DbSet<Company> Companies { get; set; }
		
		//To avoid database schema upgrade using command line
		//No DBSet for Analysis Data, 
		//No DBSet for Web Data, 

		private DataContext()
			: base()
		{

		}

		public DataContext(string connectionString)
			: base(connectionString)
		{
			_objectCtx = (this as IObjectContextAdapter).ObjectContext;
			_objectCtx.CommandTimeout = 300;
		}



		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{

            modelBuilder.Configurations.Add<Company>(new CompanyMapping());
            modelBuilder.Configurations.Add<Eod>(new EodMapping());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Entity<Eod>().MapToStoredProcedures();
			modelBuilder.Entity<Company>().MapToStoredProcedures();
		}

		#region Update
		public int LoadData(string file, string table)
		{
            const string _BULK_INSERT = @"BULK INSERT {0} FROM '{1}' WITH ( 
FIELDTERMINATOR = ',',
ROWTERMINATOR = '\n'
); SELECT @@ROWCOUNT";
            int rowCount = _objectCtx.ExecuteStoreQuery<int>
				(string.Format(_BULK_INSERT, table, file)).Single();
			return rowCount;
		}

		public List<DataState> LoadEodState()
		{
			return _objectCtx.ExecuteStoreQuery<DataState>(
				"EXEC Proc_EodState_Get").Distinct().ToList();
		}
		#endregion

		#region Analyse
		public List<DataState> LoadFullIndicatorState(string name)
		{
			return _objectCtx
                .ExecuteStoreQuery<DataState>("EXEC Proc_IndicatorState_Get_FullScan {0}", name)
				.ToList();		
		}

		public IEnumerable<ComputedEod> LoadComputedEod (string symbol = null)
		{
            return _objectCtx.ExecuteStoreQuery<ComputedEod>("EXEC Proc_ComputedEod_Get {0}", symbol);
		}

		public void SaveIndicator (Indicator value)
		{
            this.Database.ExecuteSqlCommand("EXEC Proc_Indicator_Upsert {0}, {1}, {2}, {3}",
				value.Symbol, value.Name, value.Date, value.Data);
		}

		#endregion

		#region Monitor
		public IEnumerable<RSIPredict> LoadRSIPredict (string symbol)
		{
            throw new NotImplementedException();
		}

        public IEnumerable<Stock> LoadAllStocks()
        {
            return _objectCtx.ExecuteStoreQuery<Stock>("EXEC Proc_AllWatchedStocks_Get");
        }
		#endregion

        internal IEnumerable<DataState> LoadDailySummaryState()
        {
            return _objectCtx.ExecuteStoreQuery<DataState>("EXEC Proc_DailySummaryState_Get");
        }

        internal void SaveDailySummary(DailySummary value)
        {
            this.Database.ExecuteSqlCommand("EXEC Proc_DailySummary_Upsert {0}, {1}, {2}, {3}",
                value.Symbol, value.Date, value.Version, value.Data);
        }

        internal Eod GetLastEod(string symbol)
        {
            return _objectCtx.ExecuteStoreQuery<Eod>("EXEC Proc_Eod_GetLast {0}", symbol).FirstOrDefault();
        }

        internal DailySummary GetLastSummary(string symbol)
        {
            return _objectCtx.ExecuteStoreQuery<DailySummary>("EXEC Proc_DailySummary_GetLast {0}", symbol).FirstOrDefault();
        }

        internal Indicator GetIndicator(string symbol, string name, DateTime date)
        {
            return _objectCtx.ExecuteStoreQuery<Indicator>("EXEC Proc_Indicator_Get {0}, {1}, {2}", symbol, name, date).FirstOrDefault();
        }

        internal DailySummary GetDailySummary(string symbol, DateTime date)
        {
            return _objectCtx.ExecuteStoreQuery<DailySummary>("EXEC Proc_DailySummary_Get {0}, {1}", symbol, date).FirstOrDefault();
        }

        internal EmailArchive GetEmailArchive(string name, DateTime date)
        {
            return _objectCtx.ExecuteStoreQuery<EmailArchive>("EXEC Proc_EmailArchive_Get {0}, {1}", name, date).FirstOrDefault();
        }

        internal void SaveEmailArchive(EmailArchive value)
        {
            this.Database.ExecuteSqlCommand("EXEC Proc_EmailArchive_Upsert {0}, {1}, {2}",
                value.Name, value.DateSent, value.Html);
        }
    }
}

