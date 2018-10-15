using System.Reflection;
using System.Reflection.PortableExecutable;
using Drachenhorn.Core.Printing;
using Drachenhorn.Core.ViewModels;
using Drachenhorn.Xml.Sheet;
using Drachenhorn.Xml.Sheet.Common;
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

        [TestMethod]
        public void TestPrinting()
        {
            InitializeData();
            
            var sheet = new CharacterSheet
            {
                Characteristics = new Characteristics
                {
                    Name = "test",
                    Race = new RaceInformation {Name = "testRace"},
                    Culture = new CultureInformation {Name = "testCulture", Description = "test"},
                    Profession = new ProfessionInformation {Name = "testProfession"}
                }
            };

            var temp = PrintingManager.GenerateHtml(sheet);
        }
    }
}