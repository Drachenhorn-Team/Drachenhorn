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
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.ViewModels.Common;
using Drachenhorn.Core.ViewModels.Template;
using Drachenhorn.Xml.Template;
using Fluent;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für TemplateMainView.xaml
    /// </summary>
    public partial class TemplateMainView : RibbonWindow
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
            if (this.DataContext is TemplateMainViewModel)
            {
                var model = (TemplateMainViewModel) this.DataContext;

                if (!model.Template.HasChanged) return;

                var task = SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage(
                    LanguageManager.Translate("UI.SouldClose"),
                    LanguageManager.Translate("UI.SouldClose.Caption"),
                    LanguageManager.Translate("UI.Yes"),
                    LanguageManager.Translate("UI.No"), null);

                if (!task.Result)
                    e.Cancel = true;
            }
        }
    }
}
