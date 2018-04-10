using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSACharacterSheet.Core.IO;
using DSACharacterSheet.Core.Lang;
using DSACharacterSheet.Desktop.Lang;
using DSACharacterSheet.FileReader;
using DSACharacterSheet.FileReader.Sheet;
using Microsoft.Win32;

namespace DSACharacterSheet.Desktop.IO
{
    public class IOService : IIOService
    {
        public void SaveStringDialog(
            string fileName,
            string fileExtension,
            string fileTypeName,
            string title,
            string text,
            bool openAfterFinished = false)
        {
            var fileDialog = new SaveFileDialog()
            {
                FileName = fileName,
                Filter = fileTypeName + " (*" + fileExtension + ")|*" + fileExtension,
                FilterIndex = 1,
                AddExtension = true,
                Title = title
            };

            if (fileDialog.ShowDialog() != true) return;

            File.WriteAllText(fileDialog.FileName, text);

            if (openAfterFinished) Process.Start(fileDialog.FileName);
        }

        public string OpenStringDialog(
            string fileExtension,
            string fileTypeName,
            string title)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = fileTypeName + "(*" +
                         fileExtension + ")|*" +
                         fileExtension + "|" +
                         LanguageManager.Translate("UI.AllFiles") + "(*.*)|*.*",
                FilterIndex = 1,
                Multiselect = false,
                Title = title,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (fileDialog.ShowDialog() != true) return null;

            return File.ReadAllText(fileDialog.FileName);
        }

        public void SaveAsCharacterSheet(CharacterSheet sheet)
        {
            var fileDialog = new SaveFileDialog
            {
                FileName = string.IsNullOrEmpty(sheet.Name)
                    ? LanguageManager.Translate("CharacterSheet.SaveDialog.DefaultFileName")
                    : sheet.Name,
                Filter = LanguageManager.Translate("CharacterSheet.FileType.Name") + " (*.dsac)|*.dsac",
                FilterIndex = 1,
                AddExtension = true,
                Title = LanguageManager.Translate("CharacterSheet.SaveDialog.Title")
            };

            if (fileDialog.ShowDialog() != true) return;

            sheet.Save(fileDialog.FileName);
        }

        public CharacterSheet OpenCharacterSheet()
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = LanguageManager.Translate("CharacterSheet.FileType.Name") + " (*.dsac)|*.dsac|Alle Dateien (*.*)|*.*",
                FilterIndex = 1,
                Multiselect = false,
                Title = LanguageManager.Translate("CharacterSheet.LoadDialog.Title"),
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            return fileDialog.ShowDialog() != true ? null : CharacterSheet.Load(fileDialog.FileName);
        }
    }
}
