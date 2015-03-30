using Newtonsoft.Json;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using StockWatch.Entities.ThirdParty;
using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace StockWatch.Internet
{
	public static class InternetReader
	{
		private static readonly string _COMPANY_URL_FMT =
			@"http://www.nasdaq.com/screening/companies-by-name.aspx?letter=&exchange={0}&render=download";

		private static readonly string _EOD_URL_FMT =
			@"http://www.google.com/finance/historical?q={0}&histperiod=daily&startdate={1}&enddate={2}&output=csv";

		//SPY is used to figure out last trade date
		private static readonly string _INDEX_SYMBOL_FOR_LAST_TRADE = @"SPY";

		private static readonly string _CURPRICE_URL_FMT =
			@"http://download.finance.yahoo.com/d/quotes.csv?s={0}&f=l1&e=.csv";

		private static readonly string _ETF_URL =
			@"http://etfdb.com/screener-data/analysis.json/";
		static InternetReader()
		{
			ServicePointManager.DefaultConnectionLimit = 100;
			ServicePointManager.Expect100Continue = false;
		}
		public static string WebGet(string url)
		{
			try
			{
                /*
				using (var client = new WebClient())
				{
					client.Proxy = null;
					return client.DownloadString(url);
				}*/
				HttpWebRequest req = 
					(HttpWebRequest)HttpWebRequest.Create(url);
				req.Method = "GET";
				req.KeepAlive = true;
				req.UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; WIndows NT 9.0; en-US))";
				req.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
				req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
				req.Proxy=null;

				using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
				{
					return reader.ReadToEnd();
				}
			}
			catch (Exception e)
			{
				Logger.Instance.WarnFormat("{0} : {1}", url, e.Message);
				return string.Empty;
			}
		}


		public static List<Company> ReadCompaniesByExchange(string exchange)
		{
			string csv = WebGet(
				string.Format(_COMPANY_URL_FMT, exchange));
			string[] lines =
				csv.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			return lines.Select(l => InternetParser.ParseCompany(l, exchange)).Where(c => c != null).ToList();
		}

		public static List<Company> ReadETFAsCompanies()
		{
			string json = WebGet(_ETF_URL);
			var etfs = JsonConvert.DeserializeObject<List<ETF>>(json);
			return etfs.Select(e => new Company
				{
					Symbol = e.symbol,
					Exchange = "ETF",
					Name = e.home_page
				}).ToList();
		}

		public static DateTime? GetLastTradeDate()
		{
			DateTime start = DateTime.Today.AddDays(-30);
			DateTime end = DateTime.Today;
			string csv = WebGet(
				string.Format(_EOD_URL_FMT,
					HttpUtility.UrlEncode(_INDEX_SYMBOL_FOR_LAST_TRADE),
					start.ToString("yyyy-MM-dd"),
					end.ToString("yyyy-MM-dd")));
			if (string.IsNullOrEmpty(csv))
				return null;
			string[] lines =
				csv.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
			List<Eod> indexEodLast30Days =
				lines.Select(l => InternetParser.ParseEod(l, _INDEX_SYMBOL_FOR_LAST_TRADE)).Where(e => e != null).ToList();
			return indexEodLast30Days.Max(e => e.Date);
		}


		public static List<Eod> ReadEodBySymbol(EodParam param)
		{
			if (param.Start >= param.End) return new List<Eod>();
			string csv = WebGet(
				string.Format(_EOD_URL_FMT,
					HttpUtility.UrlEncode(param.Symbol),
					param.Start.ToString("yyyy-MM-dd"),
					param.End.ToString("yyyy-MM-dd")));
			string[] lines =
				csv.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
			return lines
				.Select(l => InternetParser.ParseEod(l, param.Symbol))
				.Where(e => (e != null && e.Date != param.Start)).ToList();
		}

		public static double ReadCurrentPrice(string symbol)
		{
			string csv = WebGet(
				string.Format(_CURPRICE_URL_FMT, symbol));
			double value;
			if (double.TryParse(csv.Trim(), out value))
				return value;
			return -1;
		}
	}
}

