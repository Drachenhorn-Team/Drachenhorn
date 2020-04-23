using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.UI;
using Drachenhorn.Core.ViewModels.Common;
using Drachenhorn.Core.ViewModels.Sheet;
using Drachenhorn.Core.ViewModels.Template;
using Drachenhorn.Desktop.Helper;
using Drachenhorn.Desktop.UI.Dialogs;
using Drachenhorn.Desktop.UserSettings;
using Drachenhorn.Xml.Data;
using Drachenhorn.Xml.Sheet;
using Enterwell.Clients.Wpf.Notifications;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Drachenhorn.Desktop.Views
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView
    {
        #region c'tor

        public MainView(IEnumerable<string> files)
        {
            InitializeComponent();

            NotificationContainer.Manager = new NotificationMessageManager();

            if (files != null && files.Any())
                Loaded += (sender, args) =>
                {
                    foreach (var file in files) OpenFile(file);
                };

            Messenger.Default.Register<NotificationMessage>(this, RecieveMessage);
        }

        #endregion

        #region Properties

        private MainViewModel Model => DataContext is MainViewModel model ? model : null;

        #endregion

        public void OpenFile(string path)
        {
            var temp = new Uri(path).LocalPath;
            if (temp.EndsWith(Constants.SheetExtension))
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
                    if (Resources["TemplateViewModel"] != null)
                        ((TemplateViewModel) ((BindingProxy) Resources["TemplateViewModel"]).Data).Template =
                            diag.SelectedTemplate;
                //TemplateGallery.ItemsSource = SheetTemplate.AvailableTemplates;
            }
        }

        private void MainView_OnClosing(object sender, CancelEventArgs e)
        {
            if (Model == null) return;

            if (!Model.CharacterSheetViewModels.Any(x => x.CurrentSheet.HasChanged)) return;

            var task = MessageFactory.NewMessage()
                .MessageTranslated("Dialog.ShouldCloseBunch")
                .Title("Dialog.ShouldClose_Caption")
                .ButtonTranslated("Dialog.Yes", 0)
                .ButtonTranslated("Dialog.No")
                .ShowMessage();

            if (task.Result != 0)
                e.Cancel = true;
        }

        private void HamburgerMenu_OnItemClick(object sender, ItemClickEventArgs e)
        {
            // set the content
            if (e.ClickedItem is HamburgerMenuItem item && item.Tag != null)
                HamburgerMenuControl.Content = item;

            // close the pane
            HamburgerMenuControl.IsPaneOpen = false;
        }

        private void BaseMetroTabControl_OnTabItemClosing(object sender, BaseMetroTabControl.TabItemClosingEventArgs e)
        {
            if (!(e.ClosingTabItem.DataContext as CharacterSheetViewModel)?.CurrentSheet?.HasChanged == true)
                return;

            var result = this.ShowModalMessageExternal(
                LanguageManager.Translate("Dialog.ShouldClose_Caption"),
                LanguageManager.Translate("Dialog.ShouldClose"),
                MessageDialogStyle.AffirmativeAndNegative,
                new MetroDialogSettings
                {
                    AffirmativeButtonText = LanguageManager.Translate("Dialog.Yes"),
                    NegativeButtonText = LanguageManager.Translate("Dialog.No"),
                    AnimateHide = false,
                    AnimateShow = false
                });

            if (result == MessageDialogResult.Negative)
                e.Cancel = true;
        }

        #region Update

        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                if (SquirrelManager.IsUpdateAvailable().Result)
                    Dispatcher?.Invoke(() =>
                    {
                        NotificationContainer.Manager.CreateMessage()
                            .Accent((SolidColorBrush) FindResource("AccentColorBrush"))
                            .Background(
                                (SolidColorBrush) FindResource("MahApps.Metro.HamburgerMenu.PaneBackgroundBrush"))
                            .HasBadge(LanguageManager.Translate("Dialog.Update"))
                            .HasMessage(LanguageManager.Translate("Dialog.UpdateAvailable"))
                            .Dismiss().WithButton(LanguageManager.Translate("Dialog.Update_Do"), DoUpdate)
                            .Dismiss().WithButton(LanguageManager.Translate("Dialog.Update_Dismiss"), null)
                            .Dismiss().WithButton(LanguageManager.Translate("Dialog.Update_Changelog"), ShowChangelog)
                            .Queue();
                    });
            });
        }

        private (string, string) FirstLineSplitter(string val)
        {
            var message = "";
            var header = val;

            if (header.Contains('\n'))
            {
                var split = header.Split('\n');
                header = split[0];
                foreach (var s in split)
                    if (s != header)
                        message += s + "\n";

                message = message.Substring(0, message.Length - 2);
            }

            return (header, message);
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
                        .HasBadge(LanguageManager.Translate("Dialog.Update"))
                        .HasMessage(LanguageManager.Translate("Dialog.Updating"))
                        .Dismiss().WithDelay(5000)
                        .Queue();
                });

                SquirrelManager.UpdateSquirrel(null, (f, x) =>
                {
                    if (f)
                        Dispatcher.Invoke(() =>
                        {
                            var temp = FirstLineSplitter(LanguageManager.Translate("Dialog.UpdateFinished"));

                            NotificationContainer.Manager.CreateMessage()
                                .Accent((SolidColorBrush) FindResource("AccentColorBrush"))
                                .Background(
                                    (SolidColorBrush) FindResource("MahApps.Metro.HamburgerMenu.PaneBackgroundBrush"))
                                .HasBadge(LanguageManager.Translate("Dialog.Update"))
                                .HasHeader(temp.Item1)
                                .HasMessage(temp.Item2)
                                .Dismiss().WithDelay(5000)
                                .Queue();
                        });
                    else
                        Dispatcher.Invoke(() =>
                        {
                            var temp = FirstLineSplitter(LanguageManager.Translate("Dialog.UpdateFailed"));

                            NotificationContainer.Manager.CreateMessage()
                                .Accent((SolidColorBrush) FindResource("AccentColorBrush"))
                                .Background(
                                    (SolidColorBrush) FindResource("MahApps.Metro.HamburgerMenu.PaneBackgroundBrush"))
                                .HasBadge(LanguageManager.Translate("Dialog.Update"))
                                .HasHeader(temp.Item1)
                                .HasMessage(temp.Item2)
                                .Dismiss().WithDelay(5000)
                                .Queue();
                        });
                });
            });
        }

        private void ShowChangelog(INotificationMessageButton button)
        {
            var dialog = new ChangelogDialog(SquirrelManager.GetReleaseNotes());

            if (dialog.ShowDialog() == true) DoUpdate(null);
        }

        #endregion Update

        #region WindowDrag

        private bool _restoreForDragMove;

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (ResizeMode != ResizeMode.CanResize &&
                    ResizeMode != ResizeMode.CanResizeWithGrip)
                    return;

                WindowState = WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
            else
            {
                _restoreForDragMove = WindowState == WindowState.Maximized;
                DragMove();
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_restoreForDragMove)
            {
                _restoreForDragMove = false;

                var point = PointToScreen(e.MouseDevice.GetPosition(this));

                var fullWidth = Width;

                WindowState = WindowState.Normal;

                Left = point.X - RestoreBounds.Width * (point.X / fullWidth);
                Top = 1;

                DragMove();
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _restoreForDragMove = false;
        }

        #endregion WindowDrag
    }
}