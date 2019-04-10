using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Drachenhorn.Xml;
using Drachenhorn.Xml.Template;
using Easy.Logger.Interfaces;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Core.Downloader
{
    public class TemplateDownloader : BindableBase
    {
        private static readonly string RawPath =
            "https://gist.githubusercontent.com/lightlike/2a26930578a805d1739fe598404e60cb/raw/";

        #region Properties

        private bool _isConnectionSuccessful = true;

        private bool _isLoading;


        private ObservableCollection<OnlineTemplate> _templates = new ObservableCollection<OnlineTemplate>();

        public bool IsConnectionSuccessful
        {
            get => _isConnectionSuccessful;
            private set
            {
                if (_isConnectionSuccessful == value)
                    return;
                _isConnectionSuccessful = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading == value)
                    return;
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<OnlineTemplate> Templates
        {
            get
            {
                if (_templates == null || !_templates.Any())
                    Task.Run(() =>
                    {
                        var templates = LoadTemplatesAsync().Result;
                        IsConnectionSuccessful = templates != null && templates.Count > 0;

                        if (IsConnectionSuccessful)
                            Templates = templates;
                    });
                return _templates;
            }
            private set
            {
                if (_templates == value)
                    return;
                _templates = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Loading

        /// <summary>
        ///     Loads the templates from an online Source
        /// </summary>
        /// <returns>true if connection is successful.</returns>
        private async Task<ObservableCollection<OnlineTemplate>> LoadTemplatesAsync()
        {
            return await Task.Run(() =>
            {
                var textFromFile = "";

                IsLoading = true;

                var result = new ObservableCollection<OnlineTemplate>();

                try
                {
                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger<TemplateDownloader>()
                        .Info("Downloading online Templates.");

                    textFromFile = new WebClient().DownloadString(RawPath);
                }
                catch (WebException e)
                {
                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger<TemplateDownloader>()
                        .Warn("Could not connect to Template-Service", e);
                    result = null;
                }

                var lines = textFromFile.Split('\r', '\n');

                if (result != null)
                    foreach (var line in lines)
                        try
                        {
                            result.Add(new OnlineTemplate(line));
                        }
                        catch (InvalidOperationException e)
                        {
                            SimpleIoc.Default.GetInstance<ILogService>().GetLogger<TemplateDownloader>()
                                .Warn("Unable to read online Link.", e);
                        }

                IsLoading = false;

                return result;
            });
        }

        #endregion Loading


        #region Download

        private bool _isDownloadInitialized;

        public void Download(OnlineTemplate template)
        {
            template.IsDownloadQueued = true;

            StartDownloads();
        }

        private void StartDownloads()
        {
            if (_isDownloadInitialized)
                return;

            _isDownloadInitialized = true;

            Task.Run(async () =>
            {
                while (Templates.Any(x => x.IsDownloadQueued))
                {
                    var nextItem = Templates.First(x => x.IsDownloadQueued);

                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger<TemplateDownloader>()
                        .Info("Downloading template: " + nextItem);

                    await nextItem.InstallAsync();
                }

                _isDownloadInitialized = false;
            });
        }

        #endregion Download
    }
}