using System.Data.Entity;

namespace StockWatch.DataAccess
{
	public static class DataContextInit
	{
		public static void RegisterContextInitializers()
		{
			Database.SetInitializer<DataContext>(new DatabaseInitializer<DataContext>());
		}
	}
}

