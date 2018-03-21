using DSACharacterSheet.Core.IO;
using DSACharacterSheet.Core.Lang;
using DSACharacterSheet.Core.Settings;
using DSACharacterSheet.Core.Settings.Update;
using DSACharacterSheet.Desktop.Dialogs;
using DSACharacterSheet.Desktop.IO;
using DSACharacterSheet.Desktop.MVVM;
using DSACharacterSheet.Desktop.UserSettings;
using DSACharacterSheet.Desktop.Views;
using DSACharacterSheet.FileReader;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using CommonServiceLocator;
using Microsoft.Win32;
using SimpleLogger;
using SimpleLogger.Logging.Handlers;
using NamedPipeClientStream = System.IO.Pipes.NamedPipeClientStream;

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

            if (!IsSingleInstance())
                Application.Current.Shutdown();

            InitializeData();

            var filePath = "";
            var args = AppDomain.CurrentDomain?.SetupInformation?.ActivationArguments?.ActivationData;
            if (args != null)
            {
                foreach (var item in args)
                {
                    var temp = new Uri(item).LocalPath;
                    if (temp.EndsWith(".dsac"))
                    {
                        filePath = temp;
                        break;
                    }
                }
            }

            MainWindow = new MainView(filePath);
            MainWindow.Show();
            splash.Close();
        }

        private void InitializeData()
        {
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            Messenger.Default.Register<Exception>(this,
                ex => { new ExceptionMessageBox(ex, ex.Message).ShowDialog(); });

            SimpleIoc.Default.Register<IIOService>(() => new IOService());

            var settings = Settings.Load();
            SimpleIoc.Default.Register<ISettings>(() => settings);
            
            if (settings.VisualTheme == VisualThemeType.Dark)
            {
                Application.Current.Resources.MergedDictionaries[0] =
                    new ResourceDictionary()
                    {
                        Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative)
                    };
            }
            else if (settings.VisualTheme == VisualThemeType.System)
            {
                bool isDark = Registry.GetValue(
                                      "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize",
                                      "AppsUseLightTheme", null)
                                  as int? == 0;

                if (isDark)
                {
                    Application.Current.Resources.MergedDictionaries[0] =
                        new ResourceDictionary()
                        {
                            Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative)
                        };
                }
            }




            Logger.LoggerHandlerManager.AddHandler(
                new FileLoggerHandler(
                    "log.txt",
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "DSACharacterSheet")));

            Logger.DefaultLevel = Logger.Level.Error;
            Logger.DebugOff();

#if DEBUG
            Logger.DebugOn();
#endif

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

        #region SingleInstance

        public bool IsProcessOpen()
        {
            if (1 < Process.GetProcesses().Count(x => x.ProcessName.Contains(Process.GetCurrentProcess().ProcessName)))
                return true;
            else
                return false;
        }

        private bool IsSingleInstance()
        {
            if (IsProcessOpen())
            {
                using (var client = new NamedPipeClientStream(AppDomain.CurrentDomain.FriendlyName))
                {
                    var text = "";
                    var args = AppDomain.CurrentDomain?.SetupInformation?.ActivationArguments?.ActivationData;
                    if (args != null)
                        foreach (var item in args)
                        {
                            var temp = new Uri(item).LocalPath;
                            if (temp.EndsWith(".dsac"))
                                text = item;
                        }

                    if (string.IsNullOrEmpty(text)) return false;

                    client.Connect();
                    using (var writer = new StreamWriter(client))
                    {
                        writer.WriteLine(text);
                    }
                }

                return false;
            }
            else
            {
                var listenThread = new Thread(Listen) { IsBackground = true };
                listenThread.Start();

                return true;
            }
        }

        private void Listen()
        {
            using (var server = new NamedPipeServerStream(AppDomain.CurrentDomain.FriendlyName))
            {
                using (var reader = new StreamReader(server))
                {
                    for (; ; )
                    {
                        server.WaitForConnection();

                        var text = reader.ReadLine();

                        Dispatcher.Invoke(() =>
                        {
                            if (MainWindow is MainView)
                            {
                                var view = (MainView)MainWindow;
                                view.OpenFile(text);
                            }
                        });

                        server.Disconnect();
                        //Dispatch the message, probably onto the thread your form
                        //  was contructed on with Form.BeginInvoke
                    }
                }
            }
        }

        #endregion SingleInstance
    }
}