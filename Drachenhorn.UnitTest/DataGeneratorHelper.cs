using System.Collections.ObjectModel;
using Drachenhorn.Xml.Sheet;
using Drachenhorn.Xml.Sheet.Common;
using Drachenhorn.Xml.Sheet.Skills;

namespace Drachenhorn.UnitTest
{
    public static class DataGeneratorHelper
    {
        internal static CharacterSheet GenerateCharacterSheet()
        {
            var sheet = new CharacterSheet();

            var values = new ObservableCollection<BonusValue>
            {
                new BonusValue {Key = "CHB", Value = 2},
                new BonusValue {Key = "KKB", Value = 2}
            };

            sheet.Characteristics.Race = new RaceInformation { BaseValues = values };

            sheet.Attributes.Add(new Attribute(sheet) { FormulaText = "[CHB] + 4" });



            return sheet;
        }
    }
}
