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
            DSADate d1;
            DSADate d2;

            //Compare are equal
            d1 = new DSADate(1, 1, 1000);
            d2 = new DSADate(1, 1, 1000);
            Assert.AreEqual(d1, d2,
                d1 + " and " + d2 + " should be equal.");

            d1 = new DSADate(1, 2, 1000);
            d2 = new DSADate(1, 1, 1000);
            Assert.AreNotEqual(d1, d2,
                d1 + " and " + d2 + " should not be equal.");

            //Compare d1 to d2

            d1 = new DSADate(1, 1, 1000);
            d2 = new DSADate(1, 1, 1000);
            Assert.AreEqual(0, d1.CompareTo(d2),
                d1 + " and " + d2 + " should be equal.");

            d1 = new DSADate(1, 1, 1001);
            d2 = new DSADate(1, 1, 1000);
            Assert.AreEqual(-1, d1.CompareTo(d2),
                d1 + " should be later than " + d2);
            Assert.AreEqual(1, d2.CompareTo(d1),
                d2 + " should be later than " + d1);

            d1 = new DSADate(1, 2, 1000);
            d2 = new DSADate(1, 1, 1000);
            Assert.AreEqual(-1, d1.CompareTo(d2),
                d1 + " should be later than " + d2);
            Assert.AreEqual(1, d2.CompareTo(d1),
                d2 + " should be later than " + d1);

            d1 = new DSADate(2, 1, 1000);
            d2 = new DSADate(1, 1, 1000);
            Assert.AreEqual(-1, d1.CompareTo(d2),
                d1 + " should be later than " + d2);
            Assert.AreEqual(1, d2.CompareTo(d1),
                d2 + " should be later than " + d1);
        }
    }
}