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
using DSACharacterSheet.Desktop.Views;
using DSACharacterSheet.Xml.Template;

namespace DSACharacterSheet.Desktop.UI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für TemplateSelectorDialog.xaml
    /// </summary>
    public partial class TemplateSelectorDialog : Window
    {
        public TemplateSelectorDialog()
        {
            InitializeComponent();
            
            TemplateList.ItemsSource = DSATemplate.AvailableTemplates;
        }

        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
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
    }
}
