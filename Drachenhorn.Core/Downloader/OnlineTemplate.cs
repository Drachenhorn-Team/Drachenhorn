using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Drachenhorn.Core.IO;
using Drachenhorn.Xml;
using Drachenhorn.Xml.Template;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Core.Downloader
{
    public class OnlineTemplate : BindableBase
    {
        #region Properties

        private string _name;

        public string Name
        {
            get { return _name; }
            private set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        private double _version;

        public double Version
        {
            get { return _version; }
            private set
            {
                if (_version == value)
                    return;
                _version = value;
                OnPropertyChanged();
            }
        }

        private string _link;

        public string Link
        {
            get { return _link; }
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
            get { return _progress; }
            private set
            {
                if (_progress == value)
                    return;
                _progress = value;
                OnPropertyChanged();
            }
        }

        private bool _isDownloadStarted = false;

        public bool IsDownloadStarted
        {
            get { return _isDownloadStarted; }
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

        #region c'tor

        public OnlineTemplate(string line)
        {
            var split = line.Split(' ');

            if (split.Length < 3)
                return;

            Link = split[0];
            Version = Double.Parse(split[1].Replace("v", ""), CultureInfo.InvariantCulture);

            for (int i = 2; i < split.Length; ++i)
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
                            DSATemplate.BaseDirectory, 
                            Name + " v" + Version.ToString(CultureInfo.InvariantCulture) + DSATemplate.Extension),
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
    }
}
