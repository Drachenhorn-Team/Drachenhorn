using DSACharacterSheet.Desktop.Settings;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace DSACharacterSheet.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für SettingsView.xaml
    /// </summary>
    public partial class PropertiesView : Window
    {
        public PropertiesView()
        {
            InitializeComponent();
            this.DataContext = PropertiesManager.Properties;
        }


        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                FlagImage.Source = new BitmapImage(
                    new Uri("pack://application:,,,/DSACharacterSheet.Core;component/Images/Flags/" + PropertiesManager.Properties.CurrentCulture.Name + ".png"));
            }
            catch (IOException)
            {
                FlagImage.Source = new BitmapImage(
                    new Uri("pack://application:,,,/DSACharacterSheet.Core;component/Images/Flags/invariant.png"));
            }
        }

        private void GitCommit_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button))
                return;

            var button = (Button)sender;
            if (button.Content.ToString() != "Application not published")
                Process.Start(new ProcessStartInfo(@"https://github.com/lightlike/DSACharacterSheet/commit/" + button.Content));
        }
    }
}
