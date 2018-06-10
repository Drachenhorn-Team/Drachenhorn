using Drachenhorn.Xml.Data.AP;
using Drachenhorn.Xml.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drachenhorn.UnitTest
{
    [TestClass]
    public class FileReaderTest
    {
        [TestMethod]
        public void DSADateFormatTest()
        {
            var date = new DSADate(1, 1, 1234);

            Assert.AreEqual("1.Praios.1234 BF", date.ToString());

            date += 3650;

            Assert.AreEqual("1.Praios.1244 BF", date.ToString());

            date -= 3650;

            Assert.AreEqual("1.Praios.1234 BF", date.ToString());
        }

        [TestMethod]
        public void APCalculationTest()
        {
            var table = new APTable();

            table.APColumns.Add(new APColumn("A", 1, 5));

            table["A"].Add(1);
            table["A"].Add(2);
            table["A"].Add(3);
            table["A"].Add(4);
            table["A"].Add(5);
            table["A"].Add(6);
            table["A"].Add(7);
            table["A"].Add(8);
            table["A"].Add(10);

            Assert.AreEqual(table.Calculate("A", -2, 9), (uint) 66);
        }
    }
}