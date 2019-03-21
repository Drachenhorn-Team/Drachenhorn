using System;
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
            string[] args = {"-p", "C:\\t t\\test.dsac"};

            string[] ext = {Constants.SheetExtension, Constants.TemplateExtension};

            var manager = new ArgumentManager(args, ext);

            Assert.IsTrue(manager.ShouldPrint, "Print not recognized");

            Assert.Equals(manager.Files.Count, 1);
        }
    }
}
