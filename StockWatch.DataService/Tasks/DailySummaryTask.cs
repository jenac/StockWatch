using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Complex.Summaries;
using StockWatch.Entities.Table;
using StockWatch.Internet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockWatch.Utility;

namespace StockWatch.DataService.Tasks
{
    public class DailySummaryTask : BaseSummaryTask
    {
        private readonly ISummaryRepository _summaryRepository;

        public DailySummaryTask(ISummaryRepository summaryRepository)
		{
            _summaryRepository = summaryRepository;
		}
        public override void Execute()
        {
            List<Stock> toProcess = _summaryRepository.Stocks.ToList();
            toProcess.ForEach(s=>SummarizeData(s));
            var today = DateTime.Today;

            var emailArchive = _summaryRepository.GetEmailArchiveForDate(typeof(DailyV001).Name, today);
            //already sent for today.
            if (emailArchive!= null)
            {
                return;
            }
            List<Stock> toReport = _summaryRepository.Stocks
                .Where(s => s.InIDB50).ToList();
            List<DailyV001> reportList =  toReport
                .Select(s => _summaryRepository.GetDailySummaryListForDate(s.Symbol, today))
                .Where(d => d != null)
                .ToList();
            if  (toReport.Count() - reportList.Count() < 10)
            {
                var sender = new EmailServiceSender(ServiceSettings.Instance.EmailSettingFile);
                var html = CompositeDailySummaryHtml(reportList);
                sender.SendEmail("lihe.chen@gmail.com",
                    ServiceSettings.Instance.EmalCc,
                    "StockWatch Daily Summary",
                    html);
                _summaryRepository.SaveEmailArchive(
                    new EmailArchive
                    {
                        Name = typeof(DailyV001).Name,
                        DateSent = today,
                        Html = html
                    });
            }
            
        }
		
		private void SummarizeData (Stock stock)
		{
            Eod lastEod = _summaryRepository.GetLastEod(stock.Symbol);
            if (lastEod == null) return;
            DailySummary lastSummary = _summaryRepository.GetLastSummary(stock.Symbol);
            if (lastSummary != null && lastEod.Date <= lastSummary.Date)
                return;

            //when extend, change code to DailyV002, DailyV003 and so on.
            var daily = new DailyV001();
            daily.Symbol = stock.Symbol;
            daily.Date = lastEod.Date;
            var adx = _summaryRepository.GetADX(daily.Symbol, daily.Date);
            if (adx == null)
                return;
            daily.ADX14 = adx.ADX14;

            var sma = _summaryRepository.GetSMA(daily.Symbol, daily.Date);
            if (sma == null)
                return;
            daily.SMAShortTerm = sma.SMA5;
            daily.SMAMidTerm = sma.SMA10;
            daily.SMALongTerm = sma.SMA20;
            
            var rsi = _summaryRepository.GetRSI(daily.Symbol, daily.Date);
            if (rsi == null)
                return;
            daily.RSI14 = rsi.LastRSI;

            var rsiPredict = _summaryRepository.GetRSIPredict(daily.Symbol, daily.Date);
            if (rsiPredict == null)
                return;
            daily.R30Price = rsiPredict.PredictRsi30Price;
            daily.R70Price = rsiPredict.PredictRsi70Price;
            
            //todo
            daily.VolumePercentAgainstAvg = 0;

            _summaryRepository.SaveDailySummary(daily.ToDailySummary());
		}

        private string CompositeDailySummaryHtml(List<DailyV001> dailys)
        {

            string html = ServiceHelper.GetHtmlTemplate();
            string css =
@"
th {
	background-color: #95d0fc;
}

td {
	padding-left: 10px;
	padding-right: 10px;
}

tr:nth-child(even) {background: #CCC}
tr:nth-child(odd) {background: #FFF}  

.number {
	text-align: right;
}

";
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Symbol</th>");
            sb.AppendLine("<th>Date</th>");
            sb.AppendLine("<th>ADX</th>");
            sb.AppendLine("<th>SMA (5)</th>");
            sb.AppendLine("<th>SMA (10)</th>");
            sb.AppendLine("<th>SMA (20)</th>");
            sb.AppendLine("<th>RSI (14)</th>"); 
            sb.AppendLine("<th>Price@RSI=30</th>"); 
            sb.AppendLine("<th>Price@RSI=70</th>"); 
            sb.AppendLine("</tr>"); 
            foreach (var daily in dailys)
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td>{0}</td>", daily.Symbol);
                sb.AppendFormat("<td>{0}</td>", daily.Date.ToString("yyyy-MM-dd"));
                sb.AppendFormat("<td class='number'>{0}</td>", daily.ADX14.MoneyFormat());
                sb.AppendFormat("<td class='number'>{0}</td>", daily.SMAShortTerm.MoneyFormat());
                sb.AppendFormat("<td class='number'>{0}</td>", daily.SMAMidTerm.MoneyFormat());
                sb.AppendFormat("<td class='number'>{0}</td>", daily.SMAShortTerm.MoneyFormat());
                sb.AppendFormat("<td class='number'>{0}</td>", daily.RSI14.MoneyFormat());
                sb.AppendFormat("<td class='number'>{0}</td>", daily.R30Price.MoneyFormat());
                sb.AppendFormat("<td class='number'>{0}</td>", daily.R70Price.MoneyFormat()); 
            }
            sb.AppendLine("</table>");
            return html.Replace(ServiceHelper.CssPlaceHolder, css).Replace(ServiceHelper.ContentPlaceHolder, sb.ToString());
        }
    }
}
