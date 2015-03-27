using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StockWatch.Entities.Test
{
    [TestClass]
    public class EntityHelperTest
    {
        [TestMethod]
        public void CanGetTableName()
        {
            Assert.IsTrue(EntityHelper.GetTableName(typeof(Eod)) == "Eod");
            Assert.IsTrue(EntityHelper.GetTableName(typeof(Company)) == "Company");
        }
    }
}
