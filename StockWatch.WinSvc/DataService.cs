﻿using StockWatch.DataAccess;
using StockWatch.DataService;
using StockWatch.DataService.Senders;
using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.WinSvc
{
    public partial class DataService : ServiceBase
    {
        public DataService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Instance.Info("Stock data service started. ");

            var serviceTask = new Task(() => Execute());
            serviceTask.Start();
        }

        protected override void OnStop()
        {
            Logger.Instance.Info("Stock data service stopped. ");
        }

        public static void Execute()
        {
            var sender = new EmailAlertSender(ConfigurationManager.AppSettings["EmailSettingFile"]);
            DataContextInit.RegisterContextInitializers();
            using (var connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["StockData"].ConnectionString))
            {
                using (var context = new DataContext(connection, false))
                {
                    context.Database.Initialize(true);//.CreateIfNotExists ();
                }
                connection.Open();

                using (var context = new DataContext(connection, false))
                {
                    var runner = new ServiceRunner(context, sender);
                    runner.Run();
                }
            }
        }
    }
}