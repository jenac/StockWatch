﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockWatch.Entities.Table
{
	public class Indicator
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Data { get; set; }
    }
}
