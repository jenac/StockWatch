using System;
using System.Data.Entity;

namespace LHM.StockWatch.DataAccess
{
	public static class DataContextInit
	{
		public static void RegisterContextInitializers()
		{
			Database.SetInitializer<DataContext>(new DatabaseInitializer<DataContext>());
		}
	}
}

