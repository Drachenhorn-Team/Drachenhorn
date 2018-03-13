using DSACharacterSheet.Core.Lang;
using DSACharacterSheet.Desktop.Dialogs;
using DSACharacterSheet.Desktop.UserSettings;
using DSACharacterSheet.Desktop.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CommonServiceLocator;
using DSACharacterSheet.Core.IO;
using DSACharacterSheet.Core.Settings;
using DSACharacterSheet.Core.Settings.Update;
using DSACharacterSheet.Desktop.IO;
using DSACharacterSheet.Desktop.MVVM;
using DSACharacterSheet.FileReader;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace DSACharacterSheet.Desktop
{
    /// <inheritdoc />
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            var window = new ExceptionMessageBox(e.Exception, "Im Programm ist ein Fehler aufgetreten.");
            window.ShowDialog();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var splash = new Splash.SplashScreen();
            splash.Show();

            InitializeData();
            
            MainWindow = new MainView();
            MainWindow.Show();
            splash.Close();
        }

        private void InitializeData()
        {
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            Messenger.Default.Register<Exception>(this, ex => { new ExceptionMessageBox(ex, ex.Message).ShowDialog(); });

            var args = AppDomain.CurrentDomain?.SetupInformation?.ActivationArguments?.ActivationData;

            if (args != null)
            {
                foreach (var item in args)
                {
                    var temp = new Uri(item).LocalPath;
                    if (temp.EndsWith(".dsac"))
                        SimpleIoc.Default.Register(() => CharacterSheet.Load(temp), "InitialSheet");
                }
            }

            SimpleIoc.Default.Register<IIOService>(() => new IOService());

            var settings = Settings.Load();
            SimpleIoc.Default.Register<ISettings>(() => settings);

            settings.CheckUpdateAsync(UpdateCheckFinished);
        }


        private void UpdateCheckFinished(object sender, UpdateCheckedEventArgs args)
        {
            if (args.IsUpdateAvailable)
                Application.Current.Dispatcher.Invoke(
                    new Action(() =>
                    {
                        MessageBox.Show(
                            LanguageManager.GetLanguageText("Update.CheckForUpdate.Finished.Successful"),
                            LanguageManager.GetLanguageText("Update.CheckForUpdate.Finished.Caption"),
                            MessageBoxButton.OK,
                            MessageBoxImage.Information,
                            MessageBoxResult.OK);
                    }));
        }
    }
}
