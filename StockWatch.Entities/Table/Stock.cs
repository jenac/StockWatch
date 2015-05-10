using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockWatch.Entities.Table
{
	public class Stock
	{
		public string Symbol { get; set; }

		public bool InPossession { get; set; }

        public bool InIDB50 { get; set; }

		public DateTime SendAlertAfter { get; set; }

        public string Data { get; set; }

	}
}

