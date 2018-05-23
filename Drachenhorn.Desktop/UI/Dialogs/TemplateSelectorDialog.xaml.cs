using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Drachenhorn.Core.Downloader;
using Drachenhorn.Desktop.Views;
using Drachenhorn.Xml.Template;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für TemplateSelectorDialog.xaml
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
                TemplateList.ItemsSource = DSATemplate.AvailableTemplates;
        }

        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (TemplateList.SelectedItem == null) return;

            var view = new TemplateMainView(DSATemplate.Load(TemplateList.SelectedItem.ToString()));

            view.Show();

            this.Close();
        }

        private void NewButton_OnClick(object sender, RoutedEventArgs e)
        {
            var view = new TemplateMainView(new DSATemplate());

            view.Show();

            this.Close();
        }

        private void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button))
                return;

            var button = (Button)sender;

            if (!(button.DataContext is OnlineTemplate))
                return;

            (OnlineList.DataContext as TemplateDownloader)?.Download((OnlineTemplate)button.DataContext);
        }
    }
}
