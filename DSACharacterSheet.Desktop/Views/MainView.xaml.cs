using System;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DSACharacterSheet.Core.ViewModels;
using DSACharacterSheet.FileReader;
using GalaSoft.MvvmLight.Messaging;
using Xceed.Wpf.AvalonDock.Layout;

namespace DSACharacterSheet.Desktop.Views
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            Messenger.Default.Register<NotificationMessage>(this, RecieveMessage);
        }

        public void OpenFile(string path)
        {
            var temp = new Uri(path).LocalPath;
            if (temp.EndsWith(".dsac"))
            {
                if (this.DataContext is MainViewModel)
                {
                    var model = (MainViewModel) this.DataContext;
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
        }

        private void GenerateHTML_Click(object sender, RoutedEventArgs e)
        {
            //var dialog = new SaveFileDialog()
            //{
            //    FileName = IsNullOrEmpty(CurrentCharacterSheet.Name) ? "Charakterbogen" : CurrentCharacterSheet.Name,
            //    Filter = "HTML-Charakterbogen (*.html)|*.html",
            //    FilterIndex = 1,
            //    AddExtension = true,
            //    Title = "HTML Generieren"
            //};

            //if (dialog.ShowDialog() != true) return;

            //try
            //{
            //    PrintingManager.GenerateHtml(CurrentCharacterSheet, dialog.FileName);

            //    Process.Start(dialog.FileName);
            //}
            //catch (PrintingException ex)
            //{
            //    new ExceptionMessageBox(ex, ex.Message).ShowDialog();
            //}
        }

        private void GeneratePdf_Click(object sender, RoutedEventArgs e)
        {
            //var dialog = new SaveFileDialog()
            //{
            //    FileName = IsNullOrEmpty(CurrentCharacterSheet.Name) ? "Charakterbogen" : CurrentCharacterSheet.Name,
            //    Filter = "PDF-Charakterbogen (*.pdf)|*.pdf",
            //    FilterIndex = 1,
            //    AddExtension = true,
            //    Title = "PDF Generieren"
            //};

            //if (dialog.ShowDialog() != true) return;

            //try
            //{
            //    PrintingManager.GeneratePdf(CurrentCharacterSheet, dialog.FileName);

            //    Process.Start(dialog.FileName);
            //}
            //catch (PrintingException ex)
            //{
            //    new ExceptionMessageBox(ex, ex.Message).ShowDialog();
            //}
        }

        private void InkPresenter_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)sender).RenderTransform = new ScaleTransform(0.1, 0.1);
        }
    }
}