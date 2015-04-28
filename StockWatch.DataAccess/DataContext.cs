using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
namespace StockWatch.DataAccess
{
	public class DataContext : DbContext
	{
		protected ObjectContext _objectCtx;
		//Raw Data
		public DbSet<Eod> Eods { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<MonitorObject> MonitorObjects { get; set; }

		//To avoid database schema upgrade using command line
		//No DBSet for Analysis Data, 
		//No DBSet for Web Data, 

		private DataContext()
			: base()
		{

		}

		public DataContext(DbConnection existingConnection, bool contextOwnsConnection)
			: base(existingConnection, contextOwnsConnection)
		{
			_objectCtx = (this as IObjectContextAdapter).ObjectContext;
			_objectCtx.CommandTimeout = 300;
		}



		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
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
			return null;
			//return _objectCtx.ExecuteStoreQuery<RSIPredict> ("CALL Proc_Indicator_Get_RSIPredict ({0})", symbol);
		}
		#endregion
	}
}

