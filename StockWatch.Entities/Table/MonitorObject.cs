using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockWatch.Entities.Table
{
	[Table("MonitorObject")]
	public class MonitorObject
	{
		[Key]
		public string Symbol { get; set; }

		public bool InPossession { get; set; }

		public DateTime SendAlertAfter { get; set; }

	}
}

