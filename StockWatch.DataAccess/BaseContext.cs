using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using StockWatch.Entities;
using System.IO;
using StockWatch.Utility;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Diagnostics;

namespace LHM.StockWatch.DataAccess
{
	/*
	public abstract class BaseContext : DbContext
	{
		protected readonly ObjectContext _objectCtx;
		public ObjectContext ObjectCtx
		{
			get { return _objectCtx; }
		}

		protected readonly BCPUtility _bcpUtility;

		public BaseContext(string connectionString)
			: base(connectionString)
		{
			_objectCtx = (this as IObjectContextAdapter).ObjectContext;
			_objectCtx.CommandTimeout = 300;
			_bcpUtility = new BCPUtility(this);
		}

		/// <summary>
		/// Bulk save entities
		/// </summary>
		/// <param name="entities">list of entites</param>
		public void BulkSave(List<IBulkInsertable> entities)
		{
			if (entities == null) throw new ArgumentException();

			if (entities.Count == 0) return;
			string table = entities[0].Table;
			string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Guid.NewGuid().ToString());
			try
			{
				File.WriteAllLines(file,
					entities.Select(c => c.ToNormalizedLine()).ToArray());

				int rowCount = _bcpUtility.BulkInsert(file, table);
				if (rowCount != entities.Count)
					throw new Exception("entities count != row count");
				Logger.Instance.InfoFormat("{0} rows inserted.", rowCount);
			}
			catch (Exception e)
			{
				Logger.Instance.Error(e.Message);
				Logger.Instance.Error(e.StackTrace);
			}
			finally
			{
				File.Delete(file);
			}

		}

		/// <summary>
		/// Create database
		/// </summary>
		public void CreateDatabase()
		{
			this.Database.Initialize(true);
		}

		/// <summary>
		/// Drop database
		/// </summary>
		public void DropDatabase()
		{
			if (Database.Exists())
			{
				string lockCmd =
					string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
						Database.Connection.Database);
				Database.ExecuteSqlCommand(lockCmd);
				Database.Delete();
			}
		}

		/// <summary>
		/// export all tables to folder
		/// </summary>
		/// <param name="folder"></param>
		public void ExportAllTables(string folder)
		{
			Logger.Instance.InfoFormat("Export database to {0} ...", folder);
			try
			{
				if (!Directory.Exists(folder))
					Directory.CreateDirectory(folder);
				Logger.Instance.Info("Export table data");
				_objectCtx.ExecuteStoreQuery<string>(
					"SELECT TABLE_NAME FROM information_schema.tables WHERE TABLE_NAME <> '__MigrationHistory'")
					.ToList()
					.ForEach(t => ExportTable(folder, t));
				Logger.Instance.Info("Export successful");
			}
			catch (Exception e)
			{
				Logger.Instance.ErrorFormat("Exception while export database: {0}", e.Message);
			}

		}

		/// <summary>
		/// import data to tables
		/// </summary>
		/// <param name="folder"></param>
		public void ImportAllTables(string folder)
		{
			try
			{
				Logger.Instance.InfoFormat("Import database from folder: {0}.", folder);
				IEnumerable<FileInfo> fileInfos = new DirectoryInfo(folder).EnumerateFiles("*", SearchOption.AllDirectories);
				if (fileInfos.Count() == 0) return;
				fileInfos.ToList().ForEach(f => ImportTable(f));
			}
			catch (Exception e)
			{
				Logger.Instance.ErrorFormat("Exception while importing database: {0}", e.Message);
			}
		}

		/// <summary>
		/// export a single table data
		/// </summary>
		/// <param name="folder"></param>
		/// <param name="table"></param>
		public void ExportTable(string folder, string table)
		{
			Logger.Instance.InfoFormat("Export table {0}", table);
			_bcpUtility.BulkExport(Path.Combine(folder, table), table);
		}

		/// <summary>
		/// Import a single table.
		/// </summary>
		/// <param name="file"></param>
		public void ImportTable(string file)
		{
			FileInfo fi = new FileInfo(file);
			ImportTable(fi);
		}

		public abstract void UpdateStructureRoutines();

		public abstract void UpdateProgramRoutines();

		public abstract void RestoreData();

		public virtual void UpdateToBaseLine()
		{
			BCPUtility bu = new BCPUtility(this);
			Stopwatch sw = Stopwatch.StartNew();
			bu.BulkImport(Path.Combine(FileSystem.GetSWFolderOnGoogleDrive(),
				ServiceSettings.Instance.BaseLineFolder, "EodBaseLine.bcp"), "Eods");
			sw.Stop();
			Logger.Instance.InfoFormat("Update to base line takes {0} s.", 
				sw.Elapsed.TotalSeconds);
		}

		protected void UpdateRoutine(SqlRoutine routine)
		{
			string bootStrapText = routine.BootStrapText;
			if (!string.IsNullOrEmpty(bootStrapText))
				this.Database.ExecuteSqlCommand(bootStrapText);
			this.Database.ExecuteSqlCommand(routine.Text);
		}

		private void ImportTable(FileInfo fileInfo)
		{
			Logger.Instance.InfoFormat("Importing {0}.", fileInfo.FullName);
			//have to using string.Format for table names
			int rowCount = _bcpUtility.BulkInsert(fileInfo.Name, fileInfo.FullName);
			if (rowCount != LineCount(fileInfo.FullName))
				throw new Exception("file line count != row count");
			Logger.Instance.InfoFormat("{0} rows imported.", rowCount);
		}



		private long LineCount(string file)
		{
			long count = 0;
			string line;
			List<string> lines = new List<string>();
			using (StreamReader reader = new StreamReader(file))
			{
				while ((line = reader.ReadLine()) != null)
				{
					if (lines.Count > 10)
						lines.Remove(lines.First());
					lines.Add(line);
					count++;
				}
			}
			return count;
		}



	}*/
}

