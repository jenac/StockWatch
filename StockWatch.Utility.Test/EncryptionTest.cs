using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StockWatch.Utility.Test
{
    [TestClass]
    public class EncryptionTest
    {
        [TestMethod]
        public void Encryption_can_encrypt_string()
        {
            string plain = "b9JQp4g3";
            
            string encrypted = Encryption.Encrypt(plain);
            string decrypted = Encryption.Decrypt(encrypted);
            Assert.IsTrue(encrypted == plain);

        }
    }
}
