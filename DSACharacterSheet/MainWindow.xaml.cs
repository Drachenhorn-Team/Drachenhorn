using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
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
using DSACharacterSheet.FileReader;
using DSACharacterSheet.Dialogs;
using Microsoft.Win32;

namespace DSACharacterSheet
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CharacterSheet CurrentSheet = new CharacterSheet();


        public MainWindow()
        {
            InitializeComponent();

            var args = AppDomain.CurrentDomain?.SetupInformation?.ActivationArguments?.ActivationData;
            if (args != null)
                foreach (var item in args)
                {
                    var temp = new Uri(item).LocalPath;
                    if (temp.EndsWith(".dsac"))
                        CurrentSheet = CharacterSheet.Load(temp);
                }

            this.DataContext = CurrentSheet;
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(CurrentSheet.FilePath))
                CurrentSheet.Save();
            else
                SaveAsCommand_Executed(sender, e);
        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var fileDialog = new SaveFileDialog
            {
                FileName = String.IsNullOrEmpty(CurrentSheet.Name) ? "Charakterbogen" : CurrentSheet.Name,
                Filter = "DSA-Charakterbogen (*.dsac)|*.dsac",
                FilterIndex = 1,
                AddExtension = true,
                Title = "Charakterbogen speichern"
            };

            if (fileDialog.ShowDialog() == true)
                CurrentSheet.Save(fileDialog.FileName);
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
                CurrentSheet = CharacterSheet.Load(fileDialog.FileName);
                this.DataContext = CurrentSheet;
            }
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentSheet = new CharacterSheet();
            this.DataContext = CurrentSheet;
        }
    }
}
