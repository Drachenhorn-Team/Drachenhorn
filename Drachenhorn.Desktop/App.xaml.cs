using Drachenhorn.Core.IO;
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.Settings;
using Drachenhorn.Desktop.IO;
using Drachenhorn.Desktop.UI.Dialogs;
using Drachenhorn.Desktop.UI.MVVM;
using Drachenhorn.Desktop.UserSettings;
using Drachenhorn.Desktop.Views;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Drachenhorn.Xml.Sheet;
using Drachenhorn.Xml.Template;
using Easy.Logger;
using Easy.Logger.Interfaces;
using NamedPipeClientStream = System.IO.Pipes.NamedPipeClientStream;
using SplashScreen = Drachenhorn.Desktop.UI.Splash.SplashScreen;

namespace Drachenhorn.Desktop
{
    /// <inheritdoc />
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private ConsoleWindow _console = new ConsoleWindow();

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            try
            {
                var logger = SimpleIoc.Default.GetInstance<ILogService>().GetLogger<App>();
                logger.Fatal("Some crash occurred.", e.Exception);
            }
            catch (InvalidOperationException) { }

            var window = new ExceptionMessageBox(e.Exception, "Im Programm ist ein Fehler aufgetreten.", true);
            window.ShowDialog();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _console.Show();

#if DEBUG
            _console.Visibility = Visibility.Visible;
#endif

            var splash = new SplashScreen();
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
                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Arguments").Info(item);

                    var temp = new Uri(item).LocalPath;
                    if (temp.EndsWith(CharacterSheet.Extension)
                        || (temp.EndsWith(DSATemplate.Extension)
                            && !temp.StartsWith(DSATemplate.BaseDirectory)))
                    {
                        filePath = temp;
                        break;
                    }
                }
            }

            if (filePath.EndsWith(DSATemplate.Extension))
            {
                new TemplateImportDialog(filePath).ShowDialog();
                filePath = "";
            }

            if (SimpleIoc.Default.GetInstance<ISettings>().IsNew)
                new ThemeChooseDialog().ShowDialog();

            MainWindow = new MainView(filePath);
            MainWindow.Show();
            splash.Close();
            
            MainWindow.Closed += (s, a) => { _console.Close(); };
        }

        private void InitializeData()
        {
            SimpleIoc.Default.Register<ILogService>(() => Log4NetService.Instance);


            SimpleIoc.Default.Register<IDialogService, DialogService>();

            Messenger.Default.Register<Exception>(this,
                ex => { new ExceptionMessageBox(ex, ex.Message).ShowDialog(); });

            SimpleIoc.Default.Register<IIoService>(() => new IoService());

            var settings = Settings.Load();

            settings.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ShowConsole")
                    if (settings.ShowConsole == true)
                        _console.Visibility = Visibility.Visible;
                    else
                        _console.Visibility = Visibility.Collapsed;

            };

            SimpleIoc.Default.Register<ISettings>(() => settings);

            SetTheme(settings.VisualTheme);
            settings.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "VisualTheme")
                    SetTheme(settings.VisualTheme);
            };
        }

        #region Theme

        public static void SetTheme(VisualThemeType theme)
        {
            if (theme == VisualThemeType.System)
            {
                var isDark = Registry.GetValue(
                    "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize",
                    "AppsUseLightTheme", null);

                theme = isDark as int? == 0 ? VisualThemeType.Black : VisualThemeType.Light;
            }

            var uri = "UI/Themes/" + theme + "Theme.xaml";

            if (string.IsNullOrEmpty(uri)) return;

            Application.Current.Resources.MergedDictionaries[0] =
                new ResourceDictionary()
                {
                    Source = new Uri(uri, UriKind.Relative)
                };
        }

        #endregion Theme

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
                            if (temp.EndsWith(CharacterSheet.Extension))
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