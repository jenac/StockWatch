using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace StockWatch.Utility.Test
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void CanLog()
        {
            string logFile = 
                Path.Combine(FileSystem.GetLogFolder(), "LoggerTest.log");
            if (File.Exists(logFile)) File.Delete(logFile);
            Logger.Instance.Info("Test Logger. ");
            Assert.IsTrue(File.Exists(logFile));
        }
    }
}
