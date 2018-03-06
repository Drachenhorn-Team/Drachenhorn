using DSACharacterSheet.Core.Printing;
using DSACharacterSheet.Core.Printing.Exceptions;
using DSACharacterSheet.Core.ViewModel;
using DSACharacterSheet.Desktop.Dialogs;
using DSACharacterSheet.Desktop.UserSettings;
using DSACharacterSheet.FileReader;
using DSACharacterSheet.FileReader.Exceptions;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using static System.String;

namespace DSACharacterSheet.Desktop.Views
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }


        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //if (!IsNullOrEmpty(CurrentCharacterSheet.FilePath))
            //    try
            //    {
            //        CurrentCharacterSheet.Save(CurrentCharacterSheet.FilePath);
            //    }
            //    catch (SheetSavingException ex)
            //    {
            //        new ExceptionMessageBox(ex, ex.Message).ShowDialog();
            //    }
            //else
            //    SaveAsCommand_Executed(sender, e);
        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //var fileDialog = new SaveFileDialog
            //{
            //    FileName = IsNullOrEmpty(CurrentCharacterSheet.Name) ? "Charakterbogen" : CurrentCharacterSheet.Name,
            //    Filter = "DSA-Charakterbogen (*.dsac)|*.dsac",
            //    FilterIndex = 1,
            //    AddExtension = true,
            //    Title = "Charakterbogen speichern"
            //};

            //if (fileDialog.ShowDialog() != true) return;

            //try
            //{
            //    CurrentCharacterSheet?.Save(fileDialog.FileName);
            //}
            //catch (SheetSavingException ex)
            //{
            //    new ExceptionMessageBox(ex, ex.Message).ShowDialog();
            //}
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //var fileDialog = new OpenFileDialog
            //{
            //    Filter = "DSA-Charakterbogen (*.dsac)|*.dsac|Alle Dateien (*.*)|*.*",
            //    FilterIndex = 1,
            //    Multiselect = false,
            //    Title = "Charakterbogen öffnen.",
            //    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            //};

            //if (fileDialog.ShowDialog(this) != true) return;

            //try
            //{
            //    CurrentCharacterSheet = CharacterSheet.Load(fileDialog.FileName);
            //}
            //catch (SheetLoadingException ex)
            //{
            //    new ExceptionMessageBox(ex, ex.Message).ShowDialog();
            //}
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //CurrentCharacterSheet = new CharacterSheet();
        }

        private void PropertiesCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            new PropertiesView().ShowDialog();
        }

        private void GenerateHTML_Click(object sender, RoutedEventArgs e)
        {
            //var dialog = new SaveFileDialog()
            //{
            //    FileName = IsNullOrEmpty(CurrentCharacterSheet.Name) ? "Charakterbogen" : CurrentCharacterSheet.Name,
            //    Filter = "HTML-Charakterbogen (*.html)|*.html",
            //    FilterIndex = 1,
            //    AddExtension = true,
            //    Title = "HTML Generieren"
            //};

            //if (dialog.ShowDialog() != true) return;

            //try
            //{
            //    PrintingManager.GenerateHtml(CurrentCharacterSheet, dialog.FileName);

            //    Process.Start(dialog.FileName);
            //}
            //catch (PrintingException ex)
            //{
            //    new ExceptionMessageBox(ex, ex.Message).ShowDialog();
            //}
        }

        private void GeneratePdf_Click(object sender, RoutedEventArgs e)
        {
            //var dialog = new SaveFileDialog()
            //{
            //    FileName = IsNullOrEmpty(CurrentCharacterSheet.Name) ? "Charakterbogen" : CurrentCharacterSheet.Name,
            //    Filter = "PDF-Charakterbogen (*.pdf)|*.pdf",
            //    FilterIndex = 1,
            //    AddExtension = true,
            //    Title = "PDF Generieren"
            //};

            //if (dialog.ShowDialog() != true) return;

            //try
            //{
            //    PrintingManager.GeneratePdf(CurrentCharacterSheet, dialog.FileName);

            //    Process.Start(dialog.FileName);
            //}
            //catch (PrintingException ex)
            //{
            //    new ExceptionMessageBox(ex, ex.Message).ShowDialog();
            //}
        }
    }
}
