using DSACharacterSheet.Desktop.Dialogs;
using DSACharacterSheet.Desktop.Settings;
using DSACharacterSheet.Desktop.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DSACharacterSheet.Desktop
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private Window mainWindow = null;

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            var window = new ExceptionMessageBox(e.Exception, "Im Programm ist ein Fehler aufgetreten.");
            window.ShowDialog();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            PropertiesManager.Initialize();
            Splash.SplashScreen splash = new Splash.SplashScreen();
            splash.Show();
            mainWindow = new MainView();
            mainWindow.Show();
            splash.Close();

            PropertiesManager.Properties.CheckUpdateAsync();
        }
    }
}
