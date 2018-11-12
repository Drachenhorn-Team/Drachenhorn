using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.ViewModels.Common;
using Drachenhorn.Core.ViewModels.Sheet;
using Drachenhorn.Desktop.UI.Dialogs;
using Drachenhorn.Desktop.UserSettings;
using Drachenhorn.Xml.Sheet;
using Drachenhorn.Xml.Template;
using Enterwell.Clients.Wpf.Notifications;
using Fluent;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Desktop.Views
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : RibbonWindow
    {
        private MainViewModel Model => DataContext is MainViewModel model ? model : null;

        public MainView(string path)
        {
            InitializeComponent();

            NotificationContainer.Manager = new NotificationMessageManager();

            //Menu.Background = SystemParameters.WindowGlassBrush != null ? SystemParameters.WindowGlassBrush : new SolidColorBrush(Colors.Green);

            if (!string.IsNullOrEmpty(path)) Loaded += (sender, args) =>
            {
                OpenFile(path);
            };

            TemplateGallery.ItemsSource = SheetTemplate.AvailableTemplates;

            Messenger.Default.Register<NotificationMessage>(this, RecieveMessage);


            SimpleIoc.Default.GetInstance<LanguageManager>().LanguageChanged += (sender, args) =>
            {
                RibbonLocalization.Current.Culture = args.NewCulture;
            };
        }

        public void OpenFile(string path)
        {
            var temp = new Uri(path).LocalPath;
            if (temp.EndsWith(CharacterSheet.Extension))
                if (DataContext is MainViewModel model)
                {
                    var sheetModel = new CharacterSheetViewModel(CharacterSheet.Load(path));
                    model.CharacterSheetViewModels.Add(sheetModel);
                    model.CurrentSheetViewModel = sheetModel;
                }

            WindowState = WindowState.Maximized;
            Activate();
        }

        private void RecieveMessage(NotificationMessage message)
        {
            if (message.Notification == "ShowSettingsView")
            {
                new SettingsView().ShowDialog();
            }
            else if (message.Notification == "ShowPrintView")
            {
                if (DataContext is MainViewModel model)
                {
                    if (model.CurrentSheetViewModel == null) return;

                    new PrintView(model.CurrentSheetViewModel.CurrentSheet).ShowDialog();
                }
            }
            else if (message.Notification == "ShowOpenTemplates")
            {
                new TemplateSelectorDialog().ShowDialog();
                TemplateGallery.ItemsSource = SheetTemplate.AvailableTemplates;
            }
            else if (message.Notification == "ShowMap")
            {
                new MapView().ShowDialog();
            }
        }

        private void MainView_OnClosing(object sender, CancelEventArgs e)
        {
            if (Model == null) return;

            if (!Model.CharacterSheetViewModels.Any(x => x.CurrentSheet.HasChanged)) return;

            var task = SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage(
                LanguageManager.Translate("UI.SouldCloseBunch"),
                LanguageManager.Translate("UI.SouldCloseBunch.Caption"),
                LanguageManager.Translate("UI.Yes"),
                LanguageManager.Translate("UI.No"), null);

            if (!task.Result)
                e.Cancel = true;
        }

        #region Update

        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                if (SquirrelManager.IsUpdateAvailable().Result)
                    NotificationContainer.Manager.CreateMessage()
                        .Accent((SolidColorBrush) this.FindResource("InfoBrush"))
                        .Background((SolidColorBrush) this.FindResource("BackgroundBrush"))
                        .HasBadge("Update")
                        .HasMessage(LanguageManager.Translate("Updater.UpdateAvailable"))
                        .Dismiss().WithButton(LanguageManager.Translate("Updater.DoUpdate"), DoUpdate)
                        .Dismiss().WithButton(LanguageManager.Translate("Updater.Dismiss"), null)
                        .Queue();
            });
        }

        private void DoUpdate(INotificationMessageButton button)
        {
            var progressBar = new ProgressBar
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Height = 3,
                BorderThickness = new Thickness(0),
                Foreground = (SolidColorBrush)this.FindResource("BorderBrush"),
                Background = Brushes.Transparent,
                IsHitTestVisible = false,
                Value = 0,
                Maximum = 100,
                Minimum = -1
            };

            var notif = NotificationContainer.Manager.CreateMessage()
                .Accent((SolidColorBrush)this.FindResource("InfoBrush"))
                .Background((SolidColorBrush)this.FindResource("BackgroundBrush"))
                .HasBadge("Update")
                .HasMessage(LanguageManager.Translate("Updater.Updating"))
                .WithOverlay(progressBar)
                .Queue();

            SquirrelManager.UpdateSquirrel(x => progressBar.Value = x, (f, x) =>
            {
                NotificationContainer.Manager.Dismiss(notif);

                if (f)
                    NotificationContainer.Manager.CreateMessage()
                        .Accent((SolidColorBrush) this.FindResource("InfoBrush"))
                        .Background((SolidColorBrush) this.FindResource("BackgroundBrush"))
                        .HasBadge("Update")
                        .HasHeader(LanguageManager.Translate("Updater.UpdateFinished"))
                        .HasMessage(LanguageManager.Translate("Updater.UpdateFinished.Sub"))
                        .Dismiss().WithDelay(5000)
                        .Queue();
                else
                    NotificationContainer.Manager.CreateMessage()
                        .Accent((SolidColorBrush)this.FindResource("InfoBrush"))
                        .Background((SolidColorBrush)this.FindResource("BackgroundBrush"))
                        .HasBadge("Update")
                        .HasHeader(LanguageManager.Translate("Updater.UpdateFailed"))
                        .HasMessage(LanguageManager.Translate("Updater.UpdateFailed.Sub"))
                        .Dismiss().WithDelay(5000)
                        .Queue();
            });
        }

        #endregion Update
    }
}