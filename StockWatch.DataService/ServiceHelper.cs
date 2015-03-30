using System;

namespace StockWatch.DataService
{
	public static class ServiceHelper
	{
		public static bool InTradingTime(DateTime time)
		{
			if (time.DayOfWeek == DayOfWeek.Saturday ||
				time.DayOfWeek == DayOfWeek.Sunday)
				return false;
			DateTime theDay = new DateTime (time.Year, time.Month, time.Day);
			DateTime begin = theDay.AddHours(8).AddMinutes(30);
			DateTime end = theDay.AddHours(15);
			return time >= begin &&
				time <= end;
		}
	}
}

