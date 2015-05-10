using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.DataService.Tasks
{
    public abstract class BaseSummaryTask: ITask
    {
        //Checking from 20:00 - 23:00, every 30 mins.
        public abstract void Execute();


        public bool TimeToExecute
        {
            get
            {
                return !ServiceHelper.InSummaryTime(DateTime.Now);
            }
        }

        public int ExecuteInterval
        {
            get { return 6; }
        }
    }
}
