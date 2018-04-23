using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using DSACharacterSheet.Core.Settings;
using Easy.Logger.Interfaces;
using GalaSoft.MvvmLight.Ioc;

namespace DSACharacterSheet.Core.Images
{
    public class ImageManager : BindableBase
    {
        private VisualThemeType _currentTheme = VisualThemeType.System;
        public VisualThemeType CurrentTheme
        {
            get { return _currentTheme; }
            set
            {
                if (Equals(_currentTheme, value))
                    return;
                _currentTheme = value;
                OnPropertyChanged(null);
            }
        }

        /// <summary>
        /// Returns the translated Text for the TranslateID.
        /// </summary>
        /// <param name="key">TranslateID</param>
        /// <returns>Translated Text</returns>
        public object this[string key] => GetImage(key);

        private readonly ResourceManager ResourceManager =
            new ResourceManager("DSACharacterSheet.Core.Lang.lang", typeof(ImageManager).Assembly);

        public object GetImage(string key)
        {
            try
            {
                return ResourceManager.GetObject(key, CultureInfo.InvariantCulture);
            }
            catch (MissingManifestResourceException)
            {
                //var logger = SimpleIoc.Default.GetInstance<IEasyLogger>();
                //logger.Debug("Missing Translation: " + identifier);
                return key;
            }
        }
    }
}
