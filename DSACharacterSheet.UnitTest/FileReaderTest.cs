using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSACharacterSheet.FileReader.Objects;

namespace DSACharacterSheet.UnitTest
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
    }
}
