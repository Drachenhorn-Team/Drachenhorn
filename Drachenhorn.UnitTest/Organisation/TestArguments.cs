using Drachenhorn.Organisation.Arguments;
using Drachenhorn.Xml.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drachenhorn.UnitTest.Organisation
{
    [TestClass]
    public class TestArguments
    {
        [TestMethod]
        public void TestArgs()
        {
            string[] args = {"test.exe", "-p", "C:\\t t\\test.dsac"};

            var manager = new ArgumentManager(args);

            Assert.IsTrue(manager.ShouldPrint, "Print not recognized");

            Assert.AreEqual(1, manager[Constants.SheetExtension].Count);
        }
    }
}