using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.Settings;
using Drachenhorn.Core.UI;
using Drachenhorn.Desktop.UserSettings;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Desktop.UserControls.Common
{
    /// <summary>
    ///     Interaktionslogik für SettingsViewControl.xaml
    /// </summary>
    public partial class SettingsViewControl : UserControl
    {
        #region c'tor

        public SettingsViewControl()
        {
            InitializeComponent();

            Loaded += (sender, args) => { LanguageComboBox_SelectionChanged(sender, null); };
        }

        #endregion

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
                var culture = SimpleIoc.Default.GetInstance<ISettings>().CurrentCulture.Name;

                var path = "pack://application:,,,/Images/Flags/" + culture + ".png";
                FlagImage.Source = new BitmapImage(new Uri(path));
            }
            catch (IOException)
            {
                var path = "pack://application:,,,/Images/Flags/invariant.png";
                FlagImage.Source = new BitmapImage(new Uri(path));
            }
        }

        private void CheckUpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateRing.IsActive = true;

            Task.Run(() => SquirrelManager.IsUpdateAvailable()
                .ContinueWith(x =>
                {
                    var result = x.Result;
                    Dispatcher.Invoke(() => AskForUpdate(result));
                }));
        }

        private void AskForUpdate(bool result)
        {
            UpdateRing.IsActive = false;

            if (result)
            {
                var task = MessageFactory.NewMessage()
                    .MessageTranslated("Dialog.UpdateAvailable")
                    .TitleTranslated("Dialog.Update")
                    .ButtonTranslated("Dialog.Update_Do", 0)
                    .ButtonTranslated("Dialog.Update_Dismiss", 1)
                    .ShowMessage();

                if (task.Result == 0)
                    Task.Run(() => SquirrelManager.UpdateSquirrel(null, (x, y) =>
                    {
                        Dispatcher?.Invoke(() =>
                        {
                            if (x)
                                MessageFactory.NewMessage()
                                    .MessageTranslated("Dialog.UpdateFinished")
                                    .TitleTranslated("Dialog.Update")
                                    .ShowMessage();
                            else
                                MessageFactory.NewMessage()
                                    .MessageTranslated("Dialog.UpdateFailed")
                                    .Title("Dialog.Update")
                                    .ShowMessage();
                        });
                    }));
            }
            else
            {
                MessageFactory.NewMessage()
                    .MessageTranslated("Dialog.UpdateNotAvailable")
                    .Title("Dialog.Update")
                    .ShowMessage();
            }
        }

        private void SettingsViewControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            //((ObjectDataProvider)Resources["SelectableTemplates"]).Refresh();
        }
    }
}