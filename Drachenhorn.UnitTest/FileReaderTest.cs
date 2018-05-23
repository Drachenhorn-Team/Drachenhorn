using System.Collections.ObjectModel;
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

            table["A"].Costs = new ObservableCollection<ushort>() {1, 2, 3, 4, 5, 6, 7, 8, 10};

            Assert.AreEqual(table.Calculate("A", -2, 9), (uint)66);
        }
    }
}