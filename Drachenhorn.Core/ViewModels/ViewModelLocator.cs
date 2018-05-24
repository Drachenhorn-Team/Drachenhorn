using Drachenhorn.Core.Downloader;
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.ViewModels.Common;
using Drachenhorn.Core.ViewModels.Sheet;
using Drachenhorn.Core.ViewModels.Template;
using Easy.Logger.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Core.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<LanguageManager>();
                SimpleIoc.Default.Register<TemplateDownloader>();
            }
            else
            {
                SimpleIoc.Default.Register(() => new LanguageManager());
                SimpleIoc.Default.Register(() => new TemplateDownloader());
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PrintViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<TemplateMainViewModel>();
        }

        public LanguageManager LanguageManager => SimpleIoc.Default.GetInstance<LanguageManager>();

        public MainViewModel MainView => SimpleIoc.Default.GetInstance<MainViewModel>();

        public PrintViewModel PrintView => SimpleIoc.Default.GetInstance<PrintViewModel>();

        public SettingsViewModel SettingsView => SimpleIoc.Default.GetInstance<SettingsViewModel>();

        public TemplateMainViewModel TemplateMainView => SimpleIoc.Default.GetInstance<TemplateMainViewModel>();

        public TemplateDownloader TemplateDownloader => SimpleIoc.Default.GetInstance<TemplateDownloader>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}