using DSACharacterSheet.Core.IO;
using DSACharacterSheet.Core.Lang;
using DSACharacterSheet.Xml.Sheet;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace DSACharacterSheet.Desktop.IO
{
    public class IoService : IIoService
    {
        public void SaveDataDialog(
            string fileName,
            string fileExtension,
            string fileTypeName,
            string title,
            byte[] data,
            bool openAfterFinished = false)
        {
            var fileDialog = GetSaveFileDialog(fileName, fileExtension, fileTypeName, title);

            if (fileDialog.ShowDialog() != true) return;

            File.WriteAllBytes(fileDialog.FileName, data);

            if (openAfterFinished) Process.Start(fileDialog.FileName);
        }

        public byte[] OpenDataDialog(
            string fileExtension,
            string fileTypeName,
            string title)
        {
            var fileDialog = GetOpenFileDialog(fileExtension, fileTypeName, title);

            if (fileDialog.ShowDialog() != true) return null;

            return File.ReadAllBytes(fileDialog.FileName);
        }

        public void SaveStringDialog(
            string fileName,
            string fileExtension,
            string fileTypeName,
            string title,
            string text,
            bool openAfterFinished = false)
        {
            var fileDialog = GetSaveFileDialog(fileName, fileExtension, fileTypeName, title);

            if (fileDialog.ShowDialog() != true) return;

            File.WriteAllText(fileDialog.FileName, text);

            if (openAfterFinished) Process.Start(fileDialog.FileName);
        }

        public string OpenStringDialog(
            string fileExtension,
            string fileTypeName,
            string title)
        {
            var fileDialog = GetOpenFileDialog(fileExtension, fileTypeName, title);

            if (fileDialog.ShowDialog() != true) return null;

            return File.ReadAllText(fileDialog.FileName);
        }

        public void SaveAsCharacterSheet(CharacterSheet sheet)
        {
            var fileDialog = GetSaveFileDialog(
                string.IsNullOrEmpty(sheet.Characteristics.Name)
                    ? LanguageManager.Translate("CharacterSheet.SaveDialog.DefaultFileName")
                    : sheet.Characteristics.Name,
                CharacterSheet.Extension,
                LanguageManager.Translate("CharacterSheet.FileType.Name"),
                LanguageManager.Translate("CharacterSheet.SaveDialog.Title"));

            if (fileDialog.ShowDialog() != true) return;

            sheet.Save(fileDialog.FileName);
        }

        public CharacterSheet OpenCharacterSheet()
        {
            var fileDialog = GetOpenFileDialog(CharacterSheet.Extension, LanguageManager.Translate("CharacterSheet.FileType.Name"),
                LanguageManager.Translate("CharacterSheet.LoadDialog.Title"));

            return fileDialog.ShowDialog() != true ? null : CharacterSheet.Load(fileDialog.FileName);
        }

        public void SaveString(string path, string text, bool writeNew = true)
        {
            if (File.Exists(path))
                File.Delete(path);

            File.WriteAllText(path, text);
        }

        #region Helper

        private SaveFileDialog GetSaveFileDialog(
            string fileName,
            string fileExtension,
            string fileTypeName,
            string title)
        {
            return new SaveFileDialog()
            {
                FileName = fileName,
                Filter = fileTypeName + " (*" + fileExtension + ")|*" + fileExtension,
                FilterIndex = 1,
                AddExtension = true,
                Title = title
            };
        }

        private OpenFileDialog GetOpenFileDialog(
            string fileExtension,
            string fileTypeName,
            string title)
        {
            return new OpenFileDialog
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
        }

        #endregion Helper
    }
}