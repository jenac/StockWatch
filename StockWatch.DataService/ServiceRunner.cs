using StockWatch.DataAccess;
using StockWatch.DataService.Senders;
using StockWatch.DataService.Workers;
using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.Threading;

namespace StockWatch.DataService
{
	public class ServiceRunner
	{
		private readonly List<IServiceWorker> _workers;

		public ServiceRunner (DataContext context, IAlertSender sender)
		{
			if (context == null)
				throw new ArgumentException ();

			_workers = new List<IServiceWorker> {
				//add more workers in future
				new DataWorker(context),	
				new MonitorWorker(context, sender),

			};
		}

		public void Run()
		{
			while(true)
			{
				foreach (IServiceWorker worker in _workers) {
					if (worker.OnDuty) {
						try {
							worker.DoWork ();
						}
						catch(Exception e) {
							Logger.Instance.Error (e.Message);
							Logger.Instance.Error (e.StackTrace);
						}
					}
				}
				Thread.Sleep (1000 * 5 * 60);
			}
		}
	}
}

