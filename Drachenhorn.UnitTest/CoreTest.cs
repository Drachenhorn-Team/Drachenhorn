using System.Collections.ObjectModel;
using System.Reflection;
using Drachenhorn.Core.Printing;
using Drachenhorn.Core.ViewModels;
using Drachenhorn.Xml.Sheet;
using Drachenhorn.Xml.Sheet.Common;
using Drachenhorn.Xml.Sheet.Enums;
using Drachenhorn.Xml.Sheet.Skills;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Characteristics = Drachenhorn.Xml.Sheet.Common.Characteristics;

namespace Drachenhorn.UnitTest
{
    [TestClass]
    public class CoreTest
    {
        private void InitializeData()
        {
            var temp = new ViewModelLocator();
        }

        //[TestMethod]
        //public void TestPrinting()
        //{
        //    InitializeData();
            
        //    var sheet = new CharacterSheet
        //    {
        //        Characteristics = new Characteristics
        //        {
        //            Name = "test",
        //            Race = new RaceInformation {Name = "testRace"},
        //            Culture = new CultureInformation {Name = "testCulture", Description = "test"},
        //            Profession = new ProfessionInformation {Name = "testProfession"}
        //        },
        //        DisAdvantages = new ObservableCollection<DisAdvantage>()
        //        {
        //            new DisAdvantage(){Name = "test", Type = DisAdvantageType.Advantage, Specialization = "test"}
        //        }
        //    };

        //    var temp = PrintingManager.GenerateHtml(sheet);
        //}
    }
}