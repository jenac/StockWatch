using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using StockWatch.Utility;


namespace StockWatch.Internet.Test
{
    /// <summary>
    /// Summary description for EmailSenderTest
    /// </summary>
    [TestClass]
    public class EmailSenderTest
    {
        [TestMethod]
        public void CanSendEmail()
        {
            try
            {
                string settingFile = Path.Combine(FileSystem.GetStcokWatchFolderOnGoogleDrive(), "EmailSetting.xml");
                using (EmailServiceSender es = new EmailServiceSender(settingFile))
                {
                    es.SendEmail("lihe.chen@gmail.com",
                    new List<string>(),
                    "EmailSenderTest",
                    "Sent from unit test");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
