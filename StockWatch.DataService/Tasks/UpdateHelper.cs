using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;
using StockWatch.Utility;
using System;
using System.IO;


namespace StockWatch.DataService.Tasks
{
	public static class UpdateHelper
	{
		//extension method for 
		public static string ToCsv(this Company value)
		{
			return string.Format ("{0},{1},{2},{3:0.00},{4:0.00},{5},{6}", 
				value.Symbol, value.Exchange, value.Name, value.LastSale, 
				value.MarketCap, value.Sector, value.Industry);
		}

		public static string ToCsv(this Eod value)
		{
			return string.Format("{0},{1},{2:0.00},{3:0.00},{4:0.00},{5:0.00},{6}",
				value.Symbol,
				value.Date.ToString("yyyy-MM-dd"),
				value.Open,
				value.High,
				value.Low,
				value.Close,
				value.Volume);
		}

		public static void BulkSave (IDataRepository repo, Type entityType, string[] lines)
		{
			string file = Path.Combine (FileSystem.GetSWTempFolder());
			FileSystem.EnsureFolder (file);
			file = Path.Combine (file, string.Format("{0}.csv", EntityHelper.GetTableName(entityType)));
			File.WriteAllLines (file, lines);
			try {
				repo.BulkInsert (file, EntityHelper.GetTableName(entityType));
			}
			finally {
				File.Delete (file);
			}
		}
	}
}

