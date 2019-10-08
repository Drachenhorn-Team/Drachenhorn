using Drachenhorn.Xml.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drachenhorn.UnitTest.XML
{
    [TestClass]
    public class DateTest
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
        public void DateCompareTest()
        {
            var day = 2;
            var month = 6;
            var year = 1008;

            Assert.IsTrue(new DSADate(day, month, year).Equals(new DSADate(day, month, year)));

            Assert.AreEqual(-1,
                new DSADate(day, month, year)
                    .CompareTo(new DSADate(day, month - 1, year + 2)));
        }
    }
}
