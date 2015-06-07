using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StockWatch.Utility.Test
{
    [TestClass]
    public class SecureStringExtensionTest
    {
        [TestMethod]
        public void CanConvertToSecureString()
        {
            var value = "1234567890";
            var secured = value.ToSecureString();
            var back = secured.ToPlainString();
            Assert.IsTrue(back == value);
        }
    }
}
