using System;
using System.Collections.ObjectModel;
using Drachenhorn.Xml.Sheet;
using Drachenhorn.Xml.Sheet.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Attribute = Drachenhorn.Xml.Sheet.Skills.Attribute;

namespace Drachenhorn.UnitTest.DataTests.Formula
{
    [TestClass]
    public class FormulaTest
    {
        private CharacterSheet _sheet;

        [TestInitialize]
        public void InitializeFormulaTest()
        {
            _sheet = new CharacterSheet();

            var values = new ObservableCollection<BonusValue>
            {
                new BonusValue {Key = "CHB", Value = 2},
                new BonusValue {Key = "KKB", Value = 2}
            };

            _sheet.Characteristics.Race = new RaceInformation { BaseValues = values };

            _sheet.Attributes.Add(new Attribute(_sheet) { FormulaText = "[CHB] + 4" });
        }

        [TestMethod]
        public void FormulaCalculationTest()
        {

            Assert.AreEqual(6, _sheet.Attributes[0].Value);
        }
    }
}
