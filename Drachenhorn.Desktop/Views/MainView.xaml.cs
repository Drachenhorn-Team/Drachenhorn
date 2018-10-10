using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.Printing;
using Drachenhorn.Core.ViewModels.Common;
using Drachenhorn.Core.ViewModels.Sheet;
using Drachenhorn.Desktop.UI.Dialogs;
using Drachenhorn.Xml.Sheet;
using Drachenhorn.Xml.Template;
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
        public MainView(string path)
        {
            InitializeComponent();

            //Menu.Background = SystemParameters.WindowGlassBrush != null ? SystemParameters.WindowGlassBrush : new SolidColorBrush(Colors.Green);

            if (!string.IsNullOrEmpty(path)) Loaded += (sender, args) =>
            {
                OpenFile(path);
                TemplateComboBox.ItemsSource = SheetTemplate.AvailableTemplates;
            };

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
            }
            else if (message.Notification == "ShowMap")
            {
                new MapView().ShowDialog();
            }
        }

        private void MainView_OnClosing(object sender, CancelEventArgs e)
        {
            if (!(DataContext is MainViewModel model)) return;

            if (!model.CharacterSheetViewModels.Any(x => x.CurrentSheet.HasChanged)) return;

            var task = SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage(
                LanguageManager.Translate("UI.SouldCloseBunch"),
                LanguageManager.Translate("UI.SouldCloseBunch.Caption"),
                LanguageManager.Translate("UI.Yes"),
                LanguageManager.Translate("UI.No"), null);

            if (!task.Result)
                e.Cancel = true;
        }
    }
}