using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.DataService.Tasks
{
    public abstract class BaseMonitorTask : ITask
    {
        public abstract void Execute();

        public bool TimeToExecute
        {
            get
            {
                return ServiceHelper.InTradingTime(DateTime.Now);
            }
        }

        public int ExecuteInterval
        {
            get { return 1; }
        }
    }
}
