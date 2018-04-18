using DSACharacterSheet.Core.Settings;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Ioc;

namespace DSACharacterSheet.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();

            this.Loaded += (sender, args) => { LanguageComboBox_SelectionChanged(sender, null); };
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void GitCommit_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button))
                return;

            var button = (Button)sender;
            if (button.Content.ToString() != "Application not published")
                Process.Start(new ProcessStartInfo(@"https://github.com/lightlike/DSACharacterSheet/commit/" + button.Content));
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FlagImage == null) return;

            try
            {
                FlagImage.Source = new BitmapImage(
                    new Uri("pack://application:,,,/DSACharacterSheet.Desktop;component/Images/Flags/" +
                            SimpleIoc.Default.GetInstance<ISettings>().CurrentCulture.Name + ".png"));
            }
            catch (IOException)
            {
                FlagImage.Source = new BitmapImage(
                    new Uri("pack://application:,,,/DSACharacterSheet.Desktop;component/Images/Flags/invariant.png"));
            }
        }
    }
}