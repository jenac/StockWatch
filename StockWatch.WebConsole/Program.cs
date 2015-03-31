using Microsoft.Owin.Hosting;
using StockWatch.WebApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.WebConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string baseAddress = ConfigurationManager.AppSettings["BaseAddress"];
                if (string.IsNullOrEmpty(baseAddress))
                    throw new Exception("Missing Configuration: BaseAddress");
                Console.WriteLine("Starting service...");
                using (WebApp.Start<Startup>(baseAddress))
                {
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }
    }
}
