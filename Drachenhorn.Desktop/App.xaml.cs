using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Drachenhorn.Core.Downloader;
using Drachenhorn.Core.IO;
using Drachenhorn.Core.Settings;
using Drachenhorn.Core.UI;
using Drachenhorn.Desktop.IO;
using Drachenhorn.Desktop.UI;
using Drachenhorn.Desktop.UI.Dialogs;
using Drachenhorn.Desktop.UI.MVVM;
using Drachenhorn.Desktop.UserSettings;
using Drachenhorn.Desktop.Views;
using Drachenhorn.Organisation.Arguments;
using Drachenhorn.Xml.Data;
using Drachenhorn.Xml.Sheet;
using Drachenhorn.Xml.Template;
using Easy.Logger;
using Easy.Logger.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro;
using Microsoft.Win32;
using SplashScreen = Drachenhorn.Desktop.UI.Splash.SplashScreen;

namespace Drachenhorn.Desktop
{
    /// <inheritdoc />
    /// <summary>
    ///     Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        #region c'tor

        public App()
        {
            SimpleIoc.Default.Register<ILogService>(() => Log4NetService.Instance);

            SquirrelManager.Startup();

            var instance = new Task<bool>(IsSingleInstance);
            instance.ContinueWith(x =>
            {
                if (!x.Result) Current.Shutdown();
            });
            instance.Start();
        }

        #endregion

        #region Properties

        private bool _isClosing;

        #endregion

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            try
            {
                var logger = SimpleIoc.Default.GetInstance<ILogService>().GetLogger<App>();
                logger.Fatal("Some crash occurred.", e.Exception);
            }
            catch (InvalidOperationException)
            {
            }

            var window = new ExceptionMessageBox(e.Exception, "Im Programm ist ein Fehler aufgetreten.", true);
            window.ShowDialog();
        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
#if DEBUG
            var console = new ConsoleWindow();

            console.Show();
            console.Visibility = Visibility.Visible;
#endif

            var splash = new SplashScreen();
            splash.Show();

            InitializeData();

            var args = InitializeArgs();

            var templates = args[Constants.TemplateExtension];

            if (templates != null)
                new TemplateImportDialog(
                    from x in templates select new TemplateMetadata(x.FullName)
                    ).ShowDialog();

            if (args.UrlScheme != null)
            {
                var temp = new OnlineTemplate(args.UrlScheme.AbsoluteUri);

                new TemplateImportDialog(temp).ShowDialog();
            }

            if (args.ShouldPrint)
                MainWindow = InitPrintView();
            else
                MainWindow = InitMainView();

            splash.Close();


            //Set isClosing for SingleInstance Pipe
            if (MainWindow != null)
                MainWindow.Closed += (s, a) => { _isClosing = true; };

            
#if DEBUG
            //Auto-Close Debug Console on Window Close
            if (MainWindow != null)
                MainWindow.Closed += (s, a) =>
                {
                    console.ShouldClose = true;
                    console.Close();
                };
#endif

            MainWindow?.Show();
        }

        #region WindowsStartup

        private Window InitMainView()
        {
            var files = SimpleIoc.Default.GetInstance<ArgumentManager>()[Constants.SheetExtension]
                ?.Select(x => x.FullName);

            return new MainView(files);
        }

        private Window InitPrintView()
        {
            var path = SimpleIoc.Default.GetInstance<ArgumentManager>()[Constants.SheetExtension].First().FullName;

            var sheet = CharacterSheet.Load(path);

            return new PrintView(sheet);
        }

        #endregion WindowStartup

        #region Init

        private void InitializeData()
        {
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            SimpleIoc.Default.Register<IUIService>(() => new UIService());

            Messenger.Default.Register<Exception>(this,
                ex => { new ExceptionMessageBox(ex, ex.Message).ShowDialog(); });

            SimpleIoc.Default.Register<IIoService>(() => new IoService());

            var settings = Settings.Load();

            Resources["Settings"] = settings;

            SimpleIoc.Default.Register<ISettings>(() => settings);

            SetAccentAndTheme(settings.AccentColor, settings.VisualTheme);
            settings.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "VisualTheme" || args.PropertyName == "AccentColor")
                    SetAccentAndTheme(settings.AccentColor, settings.VisualTheme);
            };
        }

        private ArgumentManager InitializeArgs()
        {
            var args = Environment.GetCommandLineArgs();

            //var temp = new string[temp.Length - 1];

            //for (int i = 1; i < temp.Length; ++i)
            //    temp[i - 1] = args[i];

            //args = temp;

            foreach (var s in args)
            {
                SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Arguments").Info(s);
            }

            var manager = new ArgumentManager(args);

            SimpleIoc.Default.Register(() => manager);

            return manager;
        }

        #endregion Init

        #region Theme

        public static void SetAccentAndTheme(string name, VisualThemeType theme)
        {
            Current.Dispatcher.Invoke(() =>
            {
                if (theme == VisualThemeType.System)
                {
                    var isDark = Registry.GetValue(
                        "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize",
                        "AppsUseLightTheme", null);

                    theme = isDark as int? == 0 ? VisualThemeType.Dark : VisualThemeType.Light;
                }

                var uri = "UI/Themes/Images/" + (theme == VisualThemeType.Dark ? "White" : "Black") + ".xaml";


                if (!string.IsNullOrEmpty(uri))
                {
                    var res = Current.Resources;

                    res.BeginInit();

                    foreach (DictionaryEntry dictionaryEntry in new ResourceDictionary
                    {
                        Source = new Uri(uri, UriKind.Relative)
                    })
                        if (!res.Contains(dictionaryEntry.Key))
                            res.Add(dictionaryEntry.Key, dictionaryEntry.Value);
                        else
                            res[dictionaryEntry.Key] = dictionaryEntry.Value;

                    res.EndInit();
                }

                var mahTheme = ThemeManager.GetAppTheme(theme == VisualThemeType.Dark ? "BaseDark" : "BaseLight");
                var mahAccent = ThemeManager.GetAccent(string.IsNullOrEmpty(name) ? "Emerald" : name);

                if (mahTheme != null)
                    ThemeManager.ChangeAppStyle(Current, mahAccent, mahTheme);
            });
        }

        #endregion Theme

        #region SingleInstance

        public bool IsProcessOpen()
        {
            if (1 < Process.GetProcesses().Count(x => x.ProcessName.Contains(Process.GetCurrentProcess().ProcessName)))
                return true;
            return false;
        }

        private bool IsSingleInstance()
        {
            try
            {
                if (IsProcessOpen())
                {
                    using (var client = new NamedPipeClientStream(AppDomain.CurrentDomain.FriendlyName))
                    {
                        client.Connect(1000);

                        var text = "";
                        var args = Environment.GetCommandLineArgs().Where(x => !x.Contains("squirrel"));

                        foreach (var item in args)
                        {
                            var temp = new Uri(item).LocalPath;
                            if (temp.EndsWith(Constants.SheetExtension))
                                text = item;
                        }

                        if (string.IsNullOrEmpty(text)) return false;

                        using (var writer = new StreamWriter(client))
                        {
                            writer.WriteLine(text);
                        }
                    }

                    return false;
                }
            }
            catch (TimeoutException)
            {
                var listenThread = new Thread(Listen) {IsBackground = true};
                listenThread.Start();
            }

            return true;
        }

        private void Listen()
        {
            using (var server = new NamedPipeServerStream(AppDomain.CurrentDomain.FriendlyName))
            {
                using (var reader = new StreamReader(server))
                {
                    while (!_isClosing)
                    {
                        server.WaitForConnection();

                        var text = reader.ReadLine();

                        Dispatcher.Invoke(() =>
                        {
                            if (MainWindow is MainView view) view.OpenFile(text);
                        });

                        server.Disconnect();
                    }
                }
            }
        }

        #endregion SingleInstance
    }
}