using System;
using log4net.Config;
using log4net;
using System.Reflection;

namespace StockWatch.Utility
{
	public static class Logger
	{
		private static readonly ILog _instance;

		static Logger()
		{
			XmlConfigurator.Configure();
			_instance = LogManager.GetLogger(Assembly.GetExecutingAssembly().FullName);
		}

		public static ILog Instance
		{
			get
			{
				return _instance;
			}
		}
	}
}

