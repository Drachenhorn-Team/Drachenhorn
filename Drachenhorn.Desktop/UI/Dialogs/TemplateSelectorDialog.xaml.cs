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
    public partial class TemplateSelectorDialog
    {
        public SheetTemplate SelectedTemplate { get; private set; }

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
            SelectedTemplate = new SheetTemplate();
            DialogResult = true;

            Close();
        }

        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            TemplateMetadata data;

            if (sender is Button button)
                data = button.DataContext as TemplateMetadata;
            else
                data = TemplateList.SelectedItem as TemplateMetadata;
            
            SelectedTemplate = SheetTemplate.Load(data?.Path);
            DialogResult = true;

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
            
            TemplateMetadata data;

            if (sender is Button button)
                data = button.DataContext as TemplateMetadata;
            else
                data = TemplateList.SelectedItem as TemplateMetadata;

            File.Delete(Path.Combine(TemplateMetadata.BaseDirectory, data?.Name + TemplateMetadata.Extension));

            Selector_OnSelectionChanged(sender, null);
        }

        private void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (!(button?.DataContext is OnlineTemplate))
                return;

            (OnlineList.DataContext as TemplateDownloader)?.Download((OnlineTemplate) button.DataContext);
        }
    }
}