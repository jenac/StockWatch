﻿using System;
using System.Xml.Linq;
using StockWatch.Utility;

namespace StockWatch.Entities
{
	public class SMA : IndicatorDTO
	{
		public static string Name = "SMA";
		public double SMA50 { get; set; }
		public double SMA200 { get; set; }

		public override Indicator ToIndicator()
		{
			return new Indicator
			{
				Symbol = this.Symbol,
				Name = SMA.Name,
				Date = this.Date,
				Data = (new XElement("Data",
					new XElement("SMA50", this.SMA50.MoneyFormat()),
					new XElement("SMA200", this.SMA200.MoneyFormat()))).ToString()
			};
		}
	}
}
