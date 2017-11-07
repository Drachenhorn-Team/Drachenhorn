using DSACharacterSheet.Core.ViewModel;
using DSACharacterSheet.Desktop.Dialogs;
using DSACharacterSheet.FileReader;
using DSACharacterSheet.FileReader.Exceptions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DSACharacterSheet.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private CharacterSheet CurrentCharacterSheet
        {
            get
            {
                if (this.DataContext != null && !(this.DataContext is CharacterSheetViewModel))
                    return null;

                return ((CharacterSheetViewModel)this.DataContext).CurrentSheet;
            }
            set
            {
                if (this.DataContext != null && !(this.DataContext is CharacterSheetViewModel))
                    return;

                ((CharacterSheetViewModel)this.DataContext).CurrentSheet = value;
            }
        }


        public MainView()
        {
            this.DataContext = new CharacterSheetViewModel();

            InitializeComponent();

            var args = AppDomain.CurrentDomain?.SetupInformation?.ActivationArguments?.ActivationData;
            if (args != null)
                foreach (var item in args)
                {
                    var temp = new Uri(item).LocalPath;
                    if (temp.EndsWith(".dsac"))
                        CurrentCharacterSheet = CharacterSheet.Load(temp);
                }
        }


        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(CurrentCharacterSheet.FilePath))
                try
                {
                    CurrentCharacterSheet.Save(CurrentCharacterSheet.FilePath);
                }
                catch (SheetSavingException ex)
                {
                    new ExceptionMessageBox(ex, ex.Message).ShowDialog();
                }
            else
                SaveAsCommand_Executed(sender, e);
        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var fileDialog = new SaveFileDialog
            {
                FileName = String.IsNullOrEmpty(CurrentCharacterSheet.Name) ? "Charakterbogen" : CurrentCharacterSheet.Name,
                Filter = "DSA-Charakterbogen (*.dsac)|*.dsac",
                FilterIndex = 1,
                AddExtension = true,
                Title = "Charakterbogen speichern"
            };

            if (fileDialog.ShowDialog() == true)
                try
                {
                    CurrentCharacterSheet?.Save(fileDialog.FileName);
                }
                catch (SheetSavingException ex)
                {
                    new ExceptionMessageBox(ex, ex.Message).ShowDialog();
                }
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "DSA-Charakterbogen (*.dsac)|*.dsac|Alle Dateien (*.*)|*.*",
                FilterIndex = 1,
                Multiselect = false,
                Title = "Charakterbogen öffnen.",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (fileDialog.ShowDialog(this) == true)
            {
                try
                {
                    CurrentCharacterSheet = CharacterSheet.Load(fileDialog.FileName);
                }
                catch (SheetLoadingException ex)
                {
                    new ExceptionMessageBox(ex, ex.Message).ShowDialog();
                }
            }
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentCharacterSheet = new CharacterSheet();
        }
    }
}
