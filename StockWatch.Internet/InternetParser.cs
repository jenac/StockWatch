using System;
using StockWatch.Entities;
using System.Linq;
using StockWatch.Utility;

namespace StockWatch.Internet
{
	public static class InternetParser
	{
		public static Eod ParseEod(string line, string symbol)
		{
			if (line.StartsWith("Date", StringComparison.InvariantCultureIgnoreCase))
				return null;
			string[] sa = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			if (sa.Count() == 6)
			{
				return new Eod
				{
					Symbol = symbol,
					Date = DateTime.Parse(sa[0]),
					Open = TextParser.ParseFloat(sa[1]),
					High = TextParser.ParseFloat(sa[2]),
					Low = TextParser.ParseFloat(sa[3]),
					Close = TextParser.ParseFloat(sa[4]),
					Volume = TextParser.ParseDecimal(sa[5])
				};
			}
			return null;
		}

		public static Company ParseCompany(string line, string exchange)
		{
			char[] trimCompany = new char[] { '"' };
			if (line.StartsWith("\"Symbol\"", StringComparison.InvariantCultureIgnoreCase))
				return null;
			string[] sa = line.Split(new string[] { "\"," }, StringSplitOptions.RemoveEmptyEntries);
			if (sa.Count() >= 8)
			{
				return new Company
				{
					Symbol = sa[0].Trim(trimCompany),
					Name = sa[1].Trim(trimCompany).Replace(',', ' '),
					LastSale = TextParser.ParseFloat(sa[2].Trim(trimCompany)),
					MarketCap = TextParser.ParseDecimal(sa[3].Trim(trimCompany)),
					Sector = sa[6].Trim(trimCompany).Replace(',', ' '),
					Industry = sa[7].Trim(trimCompany).Replace(',', ' '),
					Exchange = exchange
				};
			}
			return null;
		}
	}
}

