using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWatch.Entities.Helper;
using StockWatch.Entities.Table;

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
