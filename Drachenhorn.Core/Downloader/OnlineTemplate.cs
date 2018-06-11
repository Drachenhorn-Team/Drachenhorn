using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Drachenhorn.Core.IO;
using Drachenhorn.Xml.Template;
using Easy.Logger.Interfaces;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Core.Downloader
{
    public class OnlineTemplate : TemplateMetadata
    {
        #region c'tor

        public OnlineTemplate(string line)
        {
            var split = line.Split(' ');

            if (split.Length < 2)
                return;

            Link = split[0];

            try
            {
                using (var sr = new StreamReader(new WebClient().OpenRead(Link)))
                {
                    sr.ReadLine();
                    var secondLine = sr.ReadLine();

                    if (!string.IsNullOrEmpty(secondLine))
                        SetVersionFromXMLLine(secondLine);
                }
            }
            catch (WebException e)
            {
                SimpleIoc.Default.GetInstance<ILogService>().GetLogger<OnlineTemplate>().Warn("Template not found.", e);
            }

            for (var i = 1; i < split.Length; ++i)
                Name += split[i] + " ";

            Name = Name.Remove(Name.Length - 1);
        }

        #endregion c'tor


        #region Download

        public async Task<bool> TryDownload()
        {
            try
            {
                var webClient = new WebClient();
                webClient.DownloadProgressChanged += (sender, args) => { Progress = args.ProgressPercentage; };
                var result = await webClient.DownloadStringTaskAsync(new Uri(Link));


                SimpleIoc.Default.GetInstance<IIoService>()
                    .SaveString(
                        Path.Combine(
                            SheetTemplate.BaseDirectory,
                            Name + SheetTemplate.Extension),
                        result);

                IsDownloadStarted = false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion Download


        #region ToString

        public override string ToString()
        {
            return Name + " v" + Version.ToString(CultureInfo.InvariantCulture) + " " + Link;
        }

        #endregion ToString

        #region Properties

        private string _link;

        public string Link
        {
            get => _link;
            private set
            {
                if (_link == value)
                    return;
                _link = value;
                OnPropertyChanged();
            }
        }

        #region Download

        private int _progress = -1;

        public int Progress
        {
            get => _progress;
            private set
            {
                if (_progress == value)
                    return;
                _progress = value;
                OnPropertyChanged();
            }
        }

        private bool _isDownloadStarted;

        public bool IsDownloadStarted
        {
            get => _isDownloadStarted;
            set
            {
                if (_isDownloadStarted == value)
                    return;
                _isDownloadStarted = value;
                OnPropertyChanged();
            }
        }

        #endregion Download

        #endregion Properties
    }
}