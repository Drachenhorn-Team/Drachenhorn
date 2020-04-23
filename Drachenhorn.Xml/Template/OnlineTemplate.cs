using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Drachenhorn.Xml.Data;

namespace Drachenhorn.Xml.Template
{
    /// <inheritdoc cref="ITemplateMetadata" />
    /// <summary>
    ///     Template for downloading Templates from the Web
    /// </summary>
    public class OnlineTemplate : BindableBase, ITemplateMetadata
    {
        #region c'tor

        /// <summary>
        ///     Constructor for Online Templates
        /// </summary>
        /// <param name="uri">Uri to Template</param>
        /// <exception cref="ArgumentException">Uri is not valid</exception>
        public OnlineTemplate(string uri)
        {
            Path = uri ?? throw new ArgumentException("Uri can not be null");

            try
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                using (var sr = new StreamReader(new WebClient().OpenRead(uri)))
                {
                    sr.ReadLine();
                    var secondLine = sr.ReadLine();

                    if (!string.IsNullOrEmpty(secondLine))
                        this.SetVersionAndNameFromXmlLine(secondLine);
                }
            }
            catch (WebException e)
            {
                throw new ArgumentException(uri + " is no valid URI.", e);
            }
        }

        #endregion c'tor

        #region Properties

        private string _path;

        /// <inheritdoc />
        public string Path
        {
            get => _path;
            private set
            {
                if (_path == value)
                    return;
                _path = value;
                OnPropertyChanged();
            }
        }


        private double _version;

        /// <inheritdoc />
        public double Version
        {
            get => _version;
            set
            {
                if (Math.Abs(_version - value) < double.Epsilon)
                    return;
                _version = value;
                OnPropertyChanged();
            }
        }

        private string _name;

        /// <inheritdoc />
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public bool IsInstalled => this.CheckInstalled();

        #endregion Properties

        #region Install

        private int _progress = -1;

        /// <summary>
        ///     Progress of the Download
        /// </summary>
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

        private bool _isDownloadQueued;

        /// <summary>
        ///     True if Download is Queued;
        /// </summary>
        public bool IsDownloadQueued
        {
            get => _isDownloadQueued;
            set
            {
                if (_isDownloadQueued == value)
                    return;
                _isDownloadQueued = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public bool Install()
        {
            return InstallAsync().Result;
        }

        /// <inheritdoc />
        public async Task<bool> InstallAsync()
        {
            try
            {
                var webClient = new WebClient();

                webClient.DownloadProgressChanged += (sender, args) => { Progress = args.ProgressPercentage; };
                webClient.Encoding = Encoding.UTF8;

                var target = System.IO.Path.Combine(
                    Constants.TemplateBaseDirectory,
                    Name + Constants.TemplateExtension);

                await webClient.DownloadFileTaskAsync(Path, target);

                IsDownloadQueued = false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion Install

        #region Equal

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is ITemplateMetadata metadata && Equals(metadata);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Version.GetHashCode() * 397) ^ (Name != null ? Name.GetHashCode() : 1);
            }
        }

        /// <inheritdoc />
        public bool Equals(ITemplateMetadata obj)
        {
            if (obj == null) return false;

            return Name == obj.Name && Math.Abs(Version - obj.Version) < double.Epsilon;
        }

        #endregion Equal
    }
}