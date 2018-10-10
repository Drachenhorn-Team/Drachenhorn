using System.IO;
using System.Windows;
using System.Windows.Controls;
using Drachenhorn.Core.Downloader;
using Drachenhorn.Core.Lang;
using Drachenhorn.Desktop.Views;
using Drachenhorn.Xml.Template;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    ///     Interaktionslogik für TemplateSelectorDialog.xaml
    /// </summary>
    public partial class TemplateSelectorDialog : Window
    {
        public TemplateSelectorDialog()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LocalTemplates.IsSelected)
                TemplateList.ItemsSource = SheetTemplate.AvailableTemplates;
        }

        private void NewButton_OnClick(object sender, RoutedEventArgs e)
        {
            var view = new TemplateMainView(new SheetTemplate());

            view.Show();

            Close();
        }

        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button))
                return;

            var data = ((Button) sender).DataContext as TemplateMetadata;

            var view = new TemplateMainView(SheetTemplate.Load(data?.Path));

            view.Show();

            Close();
        }

        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage(
                LanguageManager.Translate("File.Delete.Message"),
                LanguageManager.Translate("File.Delete.Title"),
                LanguageManager.Translate("UI.Yes"),
                LanguageManager.Translate("UI.No"), null).Result)
                return;


            if (!(sender is Button))
                return;

            var data = ((Button) sender).DataContext as TemplateMetadata;

            File.Delete(Path.Combine(SheetTemplate.BaseDirectory, data?.Name + SheetTemplate.Extension));

            Selector_OnSelectionChanged(sender, null);
        }

        private void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button))
                return;

            var button = (Button) sender;

            if (!(button.DataContext is OnlineTemplate))
                return;

            (OnlineList.DataContext as TemplateDownloader)?.Download((OnlineTemplate) button.DataContext);
        }
    }
}