using Drachenhorn.Xml.Sheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drachenhorn.UnitTest.XML.Formula
{
    [TestClass]
    public class FormulaTest
    {
        private CharacterSheet _sheet;

        [TestInitialize]
        public void InitializeFormulaTest()
        {
            _sheet = DataGeneratorHelper.GenerateCharacterSheet();
        }

        [TestMethod]
        public void FormulaCalculationTest()
        {
            Assert.AreEqual(6, _sheet.Attributes[0].Value);
        }
    }
}
