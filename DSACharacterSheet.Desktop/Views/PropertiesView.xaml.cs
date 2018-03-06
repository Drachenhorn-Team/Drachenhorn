using DSACharacterSheet.Core.Lang;
using DSACharacterSheet.Desktop.UserSettings;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using CommonServiceLocator;

namespace DSACharacterSheet.Desktop.Views
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    /// Interaktionslogik für SettingsView.xaml
    /// </summary>
    public partial class PropertiesView : Window
    {
        public PropertiesView()
        {
            InitializeComponent();

            LanguageComboBox_SelectionChanged(this, null);
        }


        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FlagImage == null) return;

            try
            {
                FlagImage.Source = new BitmapImage(
                    new Uri("pack://application:,,,/DSACharacterSheet.Core;component/Images/Flags/" + ServiceLocator.Current.GetInstance<Settings>().CurrentCulture.Name + ".png"));
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

        private void CheckForUpdate_Click(object sender, RoutedEventArgs e)
        {
            BusyIndicator.IsBusy = true;

            var settings = ServiceLocator.Current.GetInstance<Settings>();

            settings.UpdateChecked += UpdateCheckFinished;

            settings.CheckUpdateAsync();
        }

        private void UpdateCheckFinished(object sender, UpdateCheckedEventArgs args)
        {
            string text;

            if (args.IsUpdateAvailable)
                text = LanguageManager.GetLanguageText("Update.CheckForUpdate.Finished.Successful");
            else
                text = LanguageManager.GetLanguageText("Update.CheckForUpdate.Finished.Failed");


            Application.Current.Dispatcher.Invoke(
                new Action(() =>
                {
                    BusyIndicator.IsBusy = false;

                    MessageBox.Show(this,
                        text,
                        LanguageManager.GetLanguageText("Update.CheckForUpdate.Finished.Caption"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Information,
                        MessageBoxResult.OK);
                }));

            ServiceLocator.Current.GetInstance<Settings>().UpdateChecked -= UpdateCheckFinished;
        }
    }
}
