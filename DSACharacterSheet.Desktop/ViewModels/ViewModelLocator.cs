using CommonServiceLocator;
using DSACharacterSheet.Desktop.UserSettings;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace DSACharacterSheet.Desktop.ViewModels
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
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<Settings>();
            }
            else
            {
                SimpleIoc.Default.Register<Settings>(Settings.Load);
            }

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public Settings Settings => ServiceLocator.Current.GetInstance<Settings>();

        public MainViewModel MainView => ServiceLocator.Current.GetInstance<MainViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}