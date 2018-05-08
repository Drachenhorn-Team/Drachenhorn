using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DSACharacterSheet.Core.IO;
using DSACharacterSheet.Xml;
using GalaSoft.MvvmLight.Ioc;

namespace DSACharacterSheet.Core.Downloader
{
    public class TemplateDownloader : BindableBase
    {
        private static readonly string RawPath =
            "https://gist.githubusercontent.com/lightlike/2a26930578a805d1739fe598404e60cb/raw/";

        #region Properties

        private ObservableCollection<OnlineTemplate> _templates = new ObservableCollection<OnlineTemplate>();

        public ObservableCollection<OnlineTemplate> Templates
        {
            get
            {
                if (!_templates.Any()) LoadTemplates();
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

        #endregion Properties

        #region Loading

        private void LoadTemplates()
        {
            var textFromFile = (new WebClient()).DownloadString(RawPath);

            var lines = textFromFile.Split(new[] { '\r', '\n' });

            foreach (var line in lines)
            {
                _templates.Add(new OnlineTemplate(line));
            }
        }

        #endregion Loading


        #region Download

        private bool _isDownloadInitialized = false;

        public void Download(OnlineTemplate template)
        {
            template.IsDownloadStarted = true;

            StartDownloads();
        }

        private void StartDownloads()
        {
            if (_isDownloadInitialized)
                return;

            _isDownloadInitialized = true;

            Task.Run(() =>
            {
                while (Templates.Any(x => x.IsDownloadStarted))
                {
                    var nextItem = Templates.First(x => x.IsDownloadStarted);

                    nextItem.TryDownload();
                }

                _isDownloadInitialized = false;
            });
        }

        #endregion Download
    }
}
