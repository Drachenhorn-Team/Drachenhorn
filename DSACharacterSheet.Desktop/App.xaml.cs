using DSACharacterSheet.Core.Lang;
using DSACharacterSheet.Desktop.Dialogs;
using DSACharacterSheet.Desktop.UserSettings;
using DSACharacterSheet.Desktop.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
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
            if (!IsSingleInstance())
                Application.Current.Shutdown();

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

            Messenger.Default.Register<Exception>(this,
                ex => { new ExceptionMessageBox(ex, ex.Message).ShowDialog(); });

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
                    for (;;)
                    {
                        server.WaitForConnection();

                        var text = reader.ReadLine();
                        MessageBox.Show(text, "pipe");
                        if (MainWindow is MainView view)
                            view.OpenFile(text);

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