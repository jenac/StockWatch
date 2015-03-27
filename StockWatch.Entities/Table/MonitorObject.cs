using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockWatch.Entities
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

