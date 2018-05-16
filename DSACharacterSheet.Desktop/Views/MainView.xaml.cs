using DSACharacterSheet.Core.Printing;
using DSACharacterSheet.Core.ViewModels;
using DSACharacterSheet.Xml.Sheet;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using DSACharacterSheet.Core.Lang;
using DSACharacterSheet.Core.ViewModels.Common;
using DSACharacterSheet.Core.ViewModels.Sheet;
using DSACharacterSheet.Desktop.UI.Dialogs;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace DSACharacterSheet.Desktop.Views
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(string path)
        {
            InitializeComponent();

            //Menu.Background = SystemParameters.WindowGlassBrush != null ? SystemParameters.WindowGlassBrush : new SolidColorBrush(Colors.Green);

            if (!String.IsNullOrEmpty(path)) OpenFile(path);

            Messenger.Default.Register<NotificationMessage>(this, RecieveMessage);
        }

        public void OpenFile(string path)
        {
            var temp = new Uri(path).LocalPath;
            if (temp.EndsWith(CharacterSheet.Extension))
            {
                if (this.DataContext is MainViewModel)
                {
                    var model = (MainViewModel)this.DataContext;
                    var sheetModel = new CharacterSheetViewModel(CharacterSheet.Load(path));
                    model.CharacterSheetViewModels.Add(sheetModel);
                    model.CurrentSheetViewModel = sheetModel;
                }
            }

            this.WindowState = WindowState.Maximized;
            this.Activate();
        }

        private void RecieveMessage(NotificationMessage message)
        {
            if (message.Notification == "ShowSettingsView")
                new SettingsView().ShowDialog();
            else if (message.Notification == "ShowPrintView")
            {
                if (this.DataContext is MainViewModel)
                {
                    var model = (MainViewModel)this.DataContext;
                    var print = PrintingManager.GenerateHtml(model.CurrentSheetViewModel.CurrentSheet);
                    new PrintView(print).ShowDialog();
                }
            }
            else if (message.Notification == "ShowOpenTemplates")
                new TemplateSelectorDialog().ShowDialog();
        }

        private async void MainView_OnClosing(object sender, CancelEventArgs e)
        {
            if (this.DataContext is MainViewModel)
            {
                var model = (MainViewModel) this.DataContext;

                if (!model.CharacterSheetViewModels.Any(x => x.CurrentSheet.HasChanged)) return;

                var result = await SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage(
                    LanguageManager.Translate("UI.SouldCloseBunch"),
                    LanguageManager.Translate("UI.SouldCloseBunch.Caption"),
                    LanguageManager.Translate("UI.Yes"),
                    LanguageManager.Translate("UI.No"), null);

                if (!result)
                    e.Cancel = true;
            }
        }
    }
}