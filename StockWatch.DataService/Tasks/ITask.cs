using System;

namespace StockWatch.DataService.Tasks
{
	public interface ITask
	{
		void Execute();

        bool TimeToExecute { get; }

        int ExecuteInterval { get; }
        

	}
}

