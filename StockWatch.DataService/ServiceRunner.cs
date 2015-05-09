using StockWatch.DataAccess;
using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.Threading;

namespace StockWatch.DataService
{
    public class ServiceRunner
    {
        private readonly ServiceWorker _worker;

        public ServiceRunner(DataContext context)
        {
            if (context == null)
                throw new ArgumentException();
            _worker = new ServiceWorker(context);
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    _worker.DoWork();
                }
                catch (Exception e)
                {
                    Logger.Instance.Error(e.Message);
                    Logger.Instance.Error(e.StackTrace);
                }

                Thread.Sleep(1000 * 5 * 60);
            }
        }
    }
}

