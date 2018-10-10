using System.Collections.ObjectModel;
using Drachenhorn.Xml.Data.AP;
using Drachenhorn.Xml.Objects;
using Drachenhorn.Xml.Sheet;
using Drachenhorn.Xml.Sheet.Common;
using Drachenhorn.Xml.Sheet.Skills;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drachenhorn.UnitTest
{
    [TestClass]
    public class XmlTest
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

        [TestMethod]
        public void FormulaCalculationTest()
        {
            var sheet = new CharacterSheet();

            var values = new ObservableCollection<BonusValue>();
            values.Add(new BonusValue(){Key = "CHB", Value = 2});
            values.Add(new BonusValue(){Key = "KKB", Value = 2});

            sheet.Characteristics.Race = new RaceInformation() {BaseValues = values};

            sheet.Attributes.Add(new Attribute(sheet) {FormulaText = "[CHB] + 4"});

            Assert.AreEqual(sheet.Attributes[0].Value, 6);
        }
    }
}