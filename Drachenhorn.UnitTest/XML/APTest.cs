using Drachenhorn.Xml.Data.AP;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drachenhorn.UnitTest.XML
{
    [TestClass]
    public class APTest
    {
        private APTable _table;

        [TestInitialize]
        public void InitializeAPTest()
        {
            _table = new APTable();

            _table.APColumns.Add(new APColumn("A", 1, 5));

            _table["A"].Add(1);
            _table["A"].Add(2);
            _table["A"].Add(3);
            _table["A"].Add(4);
            _table["A"].Add(5);
            _table["A"].Add(6);
            _table["A"].Add(7);
            _table["A"].Add(8);
            _table["A"].Add(10);
        }

        [TestMethod]
        public void APCalculationTest()
        {
            Assert.AreEqual((uint) 66, _table.Calculate("A", -2, 9));
        }
    }
}