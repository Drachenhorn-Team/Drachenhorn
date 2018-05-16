using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using DSACharacterSheet.Core.Lang;
using DSACharacterSheet.Core.ViewModels.Template;
using DSACharacterSheet.Xml.Template;

namespace DSACharacterSheet.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für TemplateMainView.xaml
    /// </summary>
    public partial class TemplateMainView : Window
    {
        public TemplateMainView(DSATemplate template)
        {
            InitializeComponent();

            Loaded += (sender, args) =>
            {
                if (!(this.DataContext is TemplateMainViewModel))
                    return;

                ((TemplateMainViewModel) this.DataContext).Template = template;
            };
        }

        private void TemplateMainView_OnClosing(object sender, CancelEventArgs e)
        {
            if (((TemplateMainViewModel) this.DataContext).Template.HasChanged)
            {
                if (MessageBox.Show(this,
                        LanguageManager.Translate("UI.SouldClose"),
                        LanguageManager.Translate("UI.SouldClose.Caption"),
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning)
                    == MessageBoxResult.No)
                    e.Cancel = true;
            }
        }
    }
}
