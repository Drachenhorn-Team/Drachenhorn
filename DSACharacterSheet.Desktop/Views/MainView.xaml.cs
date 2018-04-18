using DSACharacterSheet.Core.Printing;
using DSACharacterSheet.Core.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using System.Windows.Media;
using DSACharacterSheet.Xml.Sheet;

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
            if (temp.EndsWith(".dsac"))
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
                    new PrintView(PrintingManager.GenerateHtml(model.CurrentSheetViewModel.CurrentSheet)).ShowDialog();
                }
            }
        }

        private void InkPresenter_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)sender).RenderTransform = new ScaleTransform(0.1, 0.1);
        }
    }
}