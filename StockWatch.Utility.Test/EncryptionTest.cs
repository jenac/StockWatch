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
            string plain = "Aa1234%^&*()-+";
            string pwd = "Aa1234%^&*()-+";
            string encrypted = Encryption.Encrypt(plain, pwd);
            string decrypted = Encryption.Decrypt(encrypted, pwd);
            Assert.IsTrue(encrypted == plain);

        }
    }
}
