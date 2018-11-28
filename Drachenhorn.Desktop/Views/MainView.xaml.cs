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
using Drachenhorn.Core.ViewModels.Template;
using Drachenhorn.Desktop.Helper;
using Drachenhorn.Desktop.UI.Dialogs;
using Drachenhorn.Desktop.UserSettings;
using Drachenhorn.Xml.Sheet;
using Drachenhorn.Xml.Template;
using Enterwell.Clients.Wpf.Notifications;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MahApps.Metro.Controls;

namespace Drachenhorn.Desktop.Views
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView
    {
        private MainViewModel Model => DataContext is MainViewModel model ? model : null;

        public MainView(string path)
        {
            InitializeComponent();

            NotificationContainer.Manager = new NotificationMessageManager();

            if (!string.IsNullOrEmpty(path)) Loaded += (sender, args) =>
            {
                OpenFile(path);
            };

            //TemplateGallery.ItemsSource = SheetTemplate.AvailableTemplates;

            Messenger.Default.Register<NotificationMessage>(this, RecieveMessage);
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
            if (message.Notification == "ShowPrintView")
            {
                if (DataContext is MainViewModel model)
                {
                    if (model.CurrentSheetViewModel == null) return;

                    new PrintView(model.CurrentSheetViewModel.CurrentSheet).ShowDialog();
                }
            }
            else if (message.Notification == "ShowOpenTemplates")
            {
                var diag = new TemplateSelectorDialog();
                if (diag.ShowDialog() == true)
                {
                    if (Resources["TemplateViewModel"] != null)
                        ((TemplateViewModel) ((BindingProxy) Resources["TemplateViewModel"]).Data).Template =
                            diag.SelectedTemplate;
                }
                //TemplateGallery.ItemsSource = SheetTemplate.AvailableTemplates;
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
                    Dispatcher.Invoke(() =>
                    {
                        NotificationContainer.Manager.CreateMessage()
                            .Accent((SolidColorBrush) FindResource("AccentColorBrush"))
                            .Background((SolidColorBrush) FindResource("MahApps.Metro.HamburgerMenu.PaneBackgroundBrush"))
                            .HasBadge(LanguageManager.Translate("Updater.Title"))
                            .HasMessage(LanguageManager.Translate("Updater.UpdateAvailable"))
                            .Dismiss().WithButton(LanguageManager.Translate("Updater.DoUpdate"), DoUpdate)
                            .Dismiss().WithButton(LanguageManager.Translate("Updater.Dismiss"), null)
                            .Queue();
                    });
            });
        }

        private void DoUpdate(INotificationMessageButton button)
        {
            Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    NotificationContainer.Manager.CreateMessage()
                        .Accent((SolidColorBrush) FindResource("AccentColorBrush"))
                        .Background((SolidColorBrush) FindResource("MahApps.Metro.HamburgerMenu.PaneBackgroundBrush"))
                        .HasBadge(LanguageManager.Translate("Updater.Title"))
                        .HasMessage(LanguageManager.Translate("Updater.Updating"))
                        .Dismiss().WithDelay(5000)
                        .Queue();
                });

                SquirrelManager.UpdateSquirrel(null, (f, x) =>
                {
                    if (f)
                        Dispatcher.Invoke(() =>
                        {
                            NotificationContainer.Manager.CreateMessage()
                                .Accent((SolidColorBrush) FindResource("AccentColorBrush"))
                                .Background((SolidColorBrush) FindResource("MahApps.Metro.HamburgerMenu.PaneBackgroundBrush"))
                                .HasBadge(LanguageManager.Translate("Updater.Title"))
                                .HasHeader(LanguageManager.Translate("Updater.UpdateFinished"))
                                .HasMessage(LanguageManager.Translate("Updater.UpdateFinished.Sub"))
                                .Dismiss().WithDelay(5000)
                                .Queue();
                        });
                    else
                        Dispatcher.Invoke(() =>
                        {
                            NotificationContainer.Manager.CreateMessage()
                                .Accent((SolidColorBrush) FindResource("AccentColorBrush"))
                                .Background((SolidColorBrush) FindResource("MahApps.Metro.HamburgerMenu.PaneBackgroundBrush"))
                                .HasBadge(LanguageManager.Translate("Updater.Title"))
                                .HasHeader(LanguageManager.Translate("Updater.UpdateFailed"))
                                .HasMessage(LanguageManager.Translate("Updater.UpdateFailed.Sub"))
                                .Dismiss().WithDelay(5000)
                                .Queue();
                        });
                });
            });
        }

        #endregion Update

        private void HamburgerMenu_OnItemClick(object sender, ItemClickEventArgs e)
        {
            // set the content
            if (e.ClickedItem is HamburgerMenuItem item && item.Tag != null)
                this.HamburgerMenuControl.Content = item;

            // close the pane
            this.HamburgerMenuControl.IsPaneOpen = false;
        }
    }
}