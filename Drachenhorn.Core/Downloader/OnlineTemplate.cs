using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
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

        public OnlineTemplate(string link)
        {
            Link = link;

            try
            {
                using (var sr = new StreamReader(new WebClient().OpenRead(link)))
                {
                    sr.ReadLine();
                    var secondLine = sr.ReadLine();

                    if (!string.IsNullOrEmpty(secondLine))
                        SetVersionAndNameFromXmlLine(secondLine);
                }
            }
            catch (WebException e)
            {
                SimpleIoc.Default.GetInstance<ILogService>().GetLogger<OnlineTemplate>().Warn("Template not found.", e);
            }
        }

        #endregion

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

        #endregion


        #region Download

        public async Task<bool> TryDownload()
        {
            try
            {
                var webClient = new WebClient();
                webClient.DownloadProgressChanged += (sender, args) => { Progress = args.ProgressPercentage; };

                //await webClient.DownloadStringTaskAsync(Link);
                //var contentType = webClient.ResponseHeaders["Content-Type"];
                //var charset = Regex.Match(contentType, "charset=([^;]+)").Groups[1].Value;

                //webClient.Encoding = Encoding.GetEncoding(charset);
                webClient.Encoding = Encoding.UTF8;
                var result = await webClient.DownloadStringTaskAsync(Link);


                SimpleIoc.Default.GetInstance<IIoService>()
                    .SaveString(
                        System.IO.Path.Combine(
                            BaseDirectory,
                            Name + Extension),
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
    }
}