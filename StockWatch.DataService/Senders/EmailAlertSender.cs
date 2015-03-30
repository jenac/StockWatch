using StockWatch.Entities.Complex;
using StockWatch.Internet;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockWatch.DataService.Senders
{
	public class EmailAlertSender : IAlertSender
	{
        private readonly string _settingFile ;
		public EmailAlertSender (string settingFile)
		{
            if (string.IsNullOrEmpty(settingFile))
                throw new ArgumentNullException();
            _settingFile = settingFile;
		}

		#region IAlertSender implementation

		public void SendAlerts (List<MonitorAlert> alerts)
		{
            var sender = new EmailSender(_settingFile);
			sender.SendEmail("lihe.chen@gmail.com", "StockWatch Monitor Alert",
				CreateAlertHtml(alerts));
		}
		#endregion

		private string CreateAlertHtml(List<MonitorAlert> alerts)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(@"<!DOCTYPE html>");
			sb.AppendLine(@"<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">");
			sb.AppendLine(@"<head>");
			sb.AppendLine(@"<meta charset=""utf-8"" />");
			sb.AppendLine(@"<style>");
			sb.AppendLine(@".sell { color: red; font-size: 16px; }");
			sb.AppendLine(@".buy { color: green; font-size: 16px; }");
			sb.AppendLine(@"</style>");
			sb.AppendLine(@"<title></title>");
			sb.AppendLine(@"</head>");
			sb.AppendLine(@"<body>");
			string style;
			foreach(MonitorAlert alert in alerts)
			{
				style = alert.Buy ? "buy" : "sell";
				sb.AppendFormat(
					@"<div class=""{0}"">{1}: {2} - <b>{3}</b>/<i>{4}</i></div><br/>",
					style, style, alert.Symbol, alert.Price.ToString("0.00"),
					alert.TargetPrice.ToString("0.00"));
			}
			sb.AppendLine(@"</body>");
			sb.AppendLine(@"</html>");
			return sb.ToString();
		}

	}
}

