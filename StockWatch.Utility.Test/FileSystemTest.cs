using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StockWatch.Utility.Test
{
    [TestClass]
    public class FileSystemTest
    {
        [TestMethod]
        public void CanGetUnitTestFolder()
        {
            string path = FileSystem.GetSWLogFolder();
            Assert.IsTrue(string.Compare(path, @"C:\ProgramData\StockWatch\Logs", true) == 0);

        }

        [TestMethod]
        public void CanToUnixPath()
        {
            Assert.IsTrue(FileSystem.ToUnixPath(@"C:\windows\") == @"C:/windows/");
        }
    }
}
