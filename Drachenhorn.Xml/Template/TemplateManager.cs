using System.Collections.Generic;
using System.IO;
using System.Linq;
using Drachenhorn.Xml.Data;

namespace Drachenhorn.Xml.Template
{
    /// <inheritdoc />
    /// <summary>
    ///     Manages all available Templates.
    /// </summary>
    public class TemplateManager : BindableBase
    {
        /// <summary>
        ///     Manager for all instances of SheetTemplates
        /// </summary>
        public static TemplateManager Manager { get; private set; } = new TemplateManager();


        #region c'tor

        private TemplateManager() { }

        #endregion c'tor

        #region Templates

        private IEnumerable<TemplateMetadata> _availableTemplates;

        /// <summary>
        ///     Gets the available templates.
        /// </summary>
        /// <value>
        ///     The available templates.
        /// </value>
        public IEnumerable<TemplateMetadata> AvailableTemplates
        {
            get
            {
                if (_availableTemplates != null)
                    return _availableTemplates;

                if (!Directory.Exists(Constants.TemplateBaseDirectory))
                    Directory.CreateDirectory(Constants.TemplateBaseDirectory);

                var result = new List<TemplateMetadata>();
                var files = Directory.GetFiles(Constants.TemplateBaseDirectory);

                foreach (var file in files)
                {
                    if (!file.EndsWith(Constants.TemplateExtension))
                        continue;

                    result.Add(new TemplateMetadata(file));
                }

                _availableTemplates = result;
                return result;
            }
        }

        /// <summary>
        ///     Gets the Template for the Path.
        /// </summary>
        /// <param name="path">Path to the template.</param>
        /// <returns>Selected TemplateMetadata-Object</returns>
        public TemplateMetadata GetTemplate(string path)
        {
            return AvailableTemplates.FirstOrDefault(x => x.Path == path);
        }

        /// <summary>
        ///     Resets the availableTemplates
        /// </summary>
        public void ResetAvailableTemplates()
        {
            _availableTemplates = null;
            OnPropertyChanged($"AvailableTemplates");
        }

        #endregion Templates
    }
}