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

        //20:00 - 23:00 except weekends is Summary Time, 
        public static bool InSummaryTime(DateTime time)
        {
            if (time.DayOfWeek == DayOfWeek.Saturday ||
                time.DayOfWeek == DayOfWeek.Sunday)
                return false;
            DateTime theDay = new DateTime(time.Year, time.Month, time.Day);
            DateTime begin = theDay.AddHours(20).AddMinutes(30);
            DateTime end = theDay.AddHours(23);
            return time >= begin &&
                time <= end;
        }

        public const string CssPlaceHolder = "[CssPlaceHolder]";
        public const string ContentPlaceHolder = "[ContentPlaceHolder";
        public static string GetHtmlTemplate()
        {
            return
@"<!DOCTYPE html>

<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta charset=""utf-8"" />
    <title></title>
    <style>
        [CssPlaceHolder]
    </style>
</head>

<body>
    [ContentPlaceHolder]
</body>
</html>";
        }

	}
}

