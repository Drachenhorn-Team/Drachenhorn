using DSACharacterSheet.Core.Downloader;
using DSACharacterSheet.Core.Printing;
using DSACharacterSheet.Core.ViewModels;
using DSACharacterSheet.Xml.Sheet;
using DSACharacterSheet.Xml.Sheet.Common;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSACharacterSheet.UnitTest
{
    [TestClass]
    public class CoreTest
    {
        private void InitializeData()
        {
            var temp = new ViewModelLocator();
        }

        [TestMethod]
        public void PrintingTest()
        {
            InitializeData();

            var sheet = new CharacterSheet()
            {
                Characteristics = new Characteristics()
                {
                    Name = "test",
                    Race = new RaceInformation() { Name = "testRace" },
                    Culture = new CultureInformation() { Name = "testCulture", Specification = "test" },
                    Profession = new ProfessionInformation() { Name = "testProfession" }
                }
            };

            var result = PrintingManager.GenerateHtml(sheet);

            //try
            //{
            //    var path = Path.Combine(Directory.GetCurrentDirectory(), "result.html");
            //    File.WriteAllText(path, result);
            //    Process.Start(path);
            //}
            //catch (Exception) { }
        }
    }
}