using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Easy.Logger.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using NuGet;
using Squirrel;
using Application = System.Windows.Application;

namespace Drachenhorn.Desktop.UserSettings
{
    public static class SquirrelManager
    {
        #region Squirrel

        private static string _newVersion;

        public static string NewVersion
        {
            get => _newVersion;
            private set
            {
                if (_newVersion == null)
                    return;
                _newVersion = value;
            }
        }


        private static string _currentVersion;

        public static string CurrentVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_currentVersion))
                {
                    try
                    {
                        using (var mgr = new UpdateManager("C:"))
                        {
                            _currentVersion = mgr.CurrentlyInstalledVersion().ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        _currentVersion = "X.X.X";

                        SimpleIoc.Default.GetInstance<ILogService>().GetLogger<Settings>()
                            .Debug("Unable to load Squirrel Version.", e);
                    }
                }

                return _currentVersion;
            }
        }

        private static readonly string GithubUpdatePath = "https://github.com/Drachenhorn-Team/Drachenhorn";

        private static IUpdateManager GetUpdateManager()
        {
            return UpdateManager.GitHubUpdateManager(GithubUpdatePath).Result;
        }

        public static void Startup()
        {
            SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Info("Startup");

            SquirrelAwareApp.HandleEvents(
                OnInitialInstall,
                OnAppUpdate,
                OnAppObsoleted,
                OnAppUninstall,
                OnFirstRun);
        }

        public static async Task<bool> IsUpdateAvailable(Action<int> progress = null)
        {
            try
            {
                SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Info("Checking for Update");
                using (var mgr = GetUpdateManager())
                {
                    var update = await mgr.CheckForUpdate(progress: progress);
                    if (update.ReleasesToApply.Any())
                    {
                        SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater")
                            .Info("Update available");

                        NewVersion = update.FutureReleaseEntry.Version.ToString();

                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                if (!e.Message.StartsWith("Update.exe not found"))
                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Warn("Error with Squirrel.", e);
            }

            return false;
        }

        public static async void UpdateSquirrel(Action<int> progress = null,
            Action<bool, SemanticVersion> finished = null)
        {
            try
            {
                SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Info("Doing Update");
                using (var mgr = GetUpdateManager())
                {
                    var release = await mgr.UpdateApp(progress);
                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Info("Update finished");
                    finished?.Invoke(true, release.Version);
                }
            }
            catch (Exception e)
            {
                if (!e.Message.StartsWith("Update.exe not found"))
                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Warn("Error with Squirrel.", e);
                finished?.Invoke(false, null);
            }
        }

        #endregion Squirrel

        #region Events

        private static void OnInitialInstall(Version version)
        {
            try
            {
                using (var mgr = new UpdateManager("C:"))
                {
                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Info("OnInitialInstall");
                    mgr.CreateShortcutForThisExe();

                    ExtractFileIcons(Path.Combine(mgr.RootAppDirectory, "icons"));

                    RegisterFileTypes(mgr.RootAppDirectory);

                    //UpdateManager.RestartAppWhenExited();
                    //Application.Current.Shutdown();
                }
            }
            catch (Exception e)
            {
                if (!e.Message.StartsWith("Update.exe not found"))
                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Warn("Error with Squirrel.", e);
            }
        }

        private static void OnAppUpdate(Version version)
        {
            SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Info("OnAppUpdate");

            using (var mgr = new UpdateManager("C:"))
            {
                ExtractFileIcons(Path.Combine(mgr.RootAppDirectory, "icons"));

                //RegisterFileTypes(mgr.RootAppDirectory);
            }

            //mgr.CreateShortcutForThisExe();
        }

        private static void OnAppObsoleted(Version version)
        {
            SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Info("OnAppObsolete");
        }

        private static void OnAppUninstall(Version version)
        {
            try
            {
                using (var mgr = new UpdateManager("C:"))
                {
                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Info("OnAppUninstall");
                    mgr.RemoveShortcutForThisExe();

                    // ReSharper disable once AssignNullToNotNullAttribute
                    var reg = new StreamReader(Assembly.GetExecutingAssembly()
                            .GetManifestResourceStream("Drachenhorn.Desktop.Resources.DrachenhornDelete.reg"))
                        .ReadToEnd();

                    var tempFile = Path.GetTempPath() + Guid.NewGuid() + ".reg";

                    File.WriteAllText(tempFile, reg);

                    // ReSharper disable once PossibleNullReferenceException
                    Process.Start("regedit.exe", "/s " + tempFile).WaitForExit();

                    Application.Current.Shutdown();
                }
            }
            catch (Exception e)
            {
                if (!e.Message.StartsWith("Update.exe not found"))
                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Warn("Error with Squirrel.", e);
            }
        }

        private static void OnFirstRun()
        {
            SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater").Info("OnAppUpdate");
        }

        #endregion Events

        #region Helper

        private static void ExtractFileIcons(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var fileNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            foreach (var fileName in fileNames)
            {
                if (!fileName.EndsWith(".ico")) continue;

                var split = fileName.Split('.');

                var filePath = Path.Combine(dir, split[split.Length - 2] + "." + split[split.Length - 1]);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (var fileStream = File.Create(filePath))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName).CopyTo(fileStream);
                }
            }
        }

        private static void RegisterFileTypes(string basePath)
        {
            if (Registry.ClassesRoot.GetSubKeyNames().All(x => x != "Drachenhorn"))
            {
                SimpleIoc.Default.GetInstance<ILogService>().GetLogger("Updater")
                    .Info("Register File Extensions: " + basePath);

                // ReSharper disable once AssignNullToNotNullAttribute
                var reg = new StreamReader(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("Drachenhorn.Desktop.Resources.Drachenhorn.reg")).ReadToEnd();

                reg = reg.Replace("%dir%", basePath.Replace("\\", "\\\\"));

                var tempFile = Path.GetTempPath() + Guid.NewGuid() + ".reg";

                File.WriteAllText(tempFile, reg);

                // ReSharper disable once PossibleNullReferenceException
                Process.Start("regedit.exe", "/s " + tempFile).WaitForExit();
            }
        }

        private static readonly string ApiUrl = "http://api.github.com/repos/drachenhorn-team/drachenhorn/";

        public static IEnumerable<string> GetReleaseNotes()
        {
            if (NewVersion == null) throw new InvalidOperationException("Can't get Release-Notes without checking for Update first.");

            var currentCommit = GetCommit(CurrentVersion);
            var newCommit = GetCommit(NewVersion);

            var commits = GetAllCommits();

            var commitArray = commits as (DateTime, string, string)[] ?? commits.ToArray();
            var startDate = commitArray.First(x => x.Item2 == currentCommit).Item1;
            var endDate = commitArray.First(x => x.Item2 == newCommit).Item1;

            return (from item in commitArray
                where item.Item1 > startDate
                      && item.Item1 < endDate
                      && !item.Item3.Contains("Merge")
                      && item.Item2 != currentCommit
                select item.Item3).Distinct();
        }

        private static string ApiGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            request.UserAgent = "Drachenhorn";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static string GetCommit(string version)
        {
            var api = ApiGet(ApiUrl + "git/refs/tags/" + version);
            var json = JObject.Parse(api);

            return json.SelectToken("object.sha").ToString();
        }

        private static IEnumerable<(DateTime, string, string)> GetAllCommits()
        {
            var result = new List<(DateTime, string, string)>();

            var api = ApiGet(ApiUrl + "commits");
            var json = JArray.Parse(api);

            foreach (var item in json.Children<JObject>())
            {
                var commit = item.GetValue("sha").ToString();
                var timestamp = DateTime.Parse(item.SelectToken("commit.author.date").ToString());
                var message = item.SelectToken("commit.message").ToString();

                result.Add((timestamp, commit, message));
            }

            return from item in result orderby item.Item1 select item;
        }
        
        #endregion Helper
    }
}