using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSACharacterSheet.FileReader;
using DSACharacterSheet.Core.Printing;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using CommonServiceLocator;
using DSACharacterSheet.Core.ViewModels;
using DSACharacterSheet.FileReader.Sheet;
using DSACharacterSheet.FileReader.Sheet.Common;
using GalaSoft.MvvmLight.Ioc;

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
                Name = "test",
                Race = new RaceInformation() { Name = "testRace"},
                Culture = new CultureInformation() { Name = "testCulture", Specification = "test"},
                Profession = new ProfessionInformation() { Name = "testProfession"}
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
