using StockWatch.DataAccess.Repositories;
using StockWatch.DataService.Workers;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using StockWatch.Internet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StockWatch.DataService.Tasks
{
	public class MonitorRSITask : ITask
	{
		private readonly IMonitorRepository _monitorRepo;
        
		public MonitorRSITask (IMonitorRepository repo)
		{
			_monitorRepo = repo;
		}
        public void Execute()
        {
            List<Stock> toProcess = _monitorRepo.Stocks.ToList();
            List<PriceAlert> alerts = toProcess
                .Where(s => s.InPossession || s.SendAlertAfter < DateTime.Now)
                .Select(s => ScanObject(s))
                .Where(n => n != null)
                .ToList();
            if (alerts != null && alerts.Count > 0)
            {
                var sender = new EmailServiceSender(WorkerSettings.Instance.EmailSettingFile);
                sender.SendEmail("lihe.chen@gmail.com", 
                    new List<string>(), 
                    "StockWatch Monitor Alert",
                    CompositePriceAlertHtml(alerts));
            }
            
        }
		
		private PriceAlert ScanObject (Stock monitorObject)
		{
			//get current price
			double curPrice = InternetReader.ReadCurrentPrice (monitorObject.Symbol);
			if (curPrice == -1) {
				return null;
			}
			//load current 30/70 price.
			RSIPredict predict = _monitorRepo.LoadRSIPredict (monitorObject.Symbol);
			if (predict == null) {
				return null;
			}
			if (monitorObject.InPossession && curPrice >= predict.PredictRsi70Price) {
				return new PriceAlert { 
					Symbol = monitorObject.Symbol,
					Price = curPrice,
					TargetPrice = predict.PredictRsi70Price,
					Buy = false
				};
			}

			if (!monitorObject.InPossession && curPrice <= predict.PredictRsi30Price) {
				return new PriceAlert {
					Symbol = monitorObject.Symbol,
					Price = curPrice,
					TargetPrice = predict.PredictRsi30Price,
					Buy = true
				};
			}

			return null;
		}

        private string CompositePriceAlertHtml(List<PriceAlert> alerts)
        {
            const string templateFile = @"PriceAlert.html";
            string html = File.ReadAllText(templateFile);
            string style;
            StringBuilder sb = new StringBuilder();
            foreach (PriceAlert alert in alerts)
            {
                style = alert.Buy ? "buy" : "sell";
                sb.AppendFormat(
                    @"<div class=""{0}"">{1}: {2} - <b>{3}</b>/<i>{4}</i></div><br/>",
                    style, style, alert.Symbol, alert.Price.ToString("0.00"),
                    alert.TargetPrice.ToString("0.00"));
            }
            return html.Replace("[PlaceHolder]", sb.ToString());
        }
    }
}

