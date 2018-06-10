using System.ComponentModel;
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.ViewModels.Template;
using Drachenhorn.Xml.Template;
using Fluent;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Desktop.Views
{
    /// <summary>
    ///     Interaktionslogik für TemplateMainView.xaml
    /// </summary>
    public partial class TemplateMainView : RibbonWindow
    {
        public TemplateMainView(SheetTemplate template)
        {
            InitializeComponent();

            Loaded += (sender, args) =>
            {
                if (!(DataContext is TemplateMainViewModel))
                    return;

                ((TemplateMainViewModel) DataContext).Template = template;
            };
        }

        private void TemplateMainView_OnClosing(object sender, CancelEventArgs e)
        {
            if (DataContext is TemplateMainViewModel)
            {
                var model = (TemplateMainViewModel) DataContext;

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