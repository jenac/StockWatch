using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockWatch.Entities.Table
{
	public class Indicator
    {
        [Key, Column(Order = 0), MaxLength(16)]
        public string Symbol { get; set; }
        [Key, Column(Order = 1), MaxLength(16)]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Data { get; set; }
    }
}
