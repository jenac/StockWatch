using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


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
                EmailSender es = new EmailSender(@"C:\Users\Jen\OneDrive\StockWatch\EmailSetting.xml");
                es.SendEmail("lihe.chen@gmail.com",
                    "EmailSenderTest",
                    "Sent from unit test");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
