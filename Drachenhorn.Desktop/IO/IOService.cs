﻿using System;
using System.Diagnostics;
using System.IO;
using Drachenhorn.Core.IO;
using Drachenhorn.Core.Lang;
using Drachenhorn.Desktop.UI.Dialogs;
using Drachenhorn.Xml.Data;
using Drachenhorn.Xml.Interfaces;
using Drachenhorn.Xml.Sheet;
using Microsoft.Win32;

namespace Drachenhorn.Desktop.IO
{
    public class IoService : IIoService
    {
        public void FileSaverDialog(
            string fileName,
            string fileExtension,
            string fileTypeName,
            string title,
            Action<string> fileSaveAction)
        {
            var fileDialog = GetSaveFileDialog(fileName, fileExtension, fileTypeName, title);

            if (fileDialog.ShowDialog() != true) return;

            fileSaveAction(fileDialog.FileName);
        }


        public void SaveDataDialog(
            string fileName,
            string fileExtension,
            string fileTypeName,
            string title,
            byte[] data,
            bool openAfterFinished = false)
        {
            FileSaverDialog(fileName, fileExtension, fileTypeName, title, x =>
            {
                File.WriteAllBytes(x, data);

                if (openAfterFinished) Process.Start(x);
            });
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

        public void SaveAs(ISavable savable, string fileName, string extension, string fileTypeName, string title)
        {
            var fileDialog = GetSaveFileDialog(fileName, extension, fileTypeName, title);

            if (fileDialog.ShowDialog() != true) return;

            savable.Save(fileDialog.FileName);
        }

        public CharacterSheet OpenCharacterSheet()
        {
            var fileDialog = GetOpenFileDialog(Constants.SheetExtension,
                LanguageManager.Translate("CharacterSheet.FileType.Name"),
                LanguageManager.Translate("CharacterSheet.LoadDialog.Title"));

            return fileDialog.ShowDialog() != true ? null : CharacterSheet.Load(fileDialog.FileName);
        }

        public void SaveString(string path, string text, bool writeNew = true)
        {
            if (writeNew && File.Exists(path))
                File.Delete(path);

            File.WriteAllText(path, text);
        }

        /// <summary>
        ///     Opens a Dialog in which the User can select a File inside a Directory.
        /// </summary>
        /// <param name="dir">Directory to show.</param>
        /// <param name="fileExtension">File-Extension to search from.</param>
        /// <param name="title">Title of the Dialog.</param>
        /// <returns>Selected File. Null if empty.</returns>
        public string OpenDirDialog(string dir, string fileExtension, string title)
        {
            var window = new DirectorySelectorDialog(dir, fileExtension, title);

            if (window.ShowDialog() == true)
                return window.SelectedFile;

            return null;
        }

        #region Helper

        private SaveFileDialog GetSaveFileDialog(
            string fileName,
            string fileExtension,
            string fileTypeName,
            string title)
        {
            return new SaveFileDialog
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