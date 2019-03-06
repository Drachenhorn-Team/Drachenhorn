using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Drachenhorn.Xml.Template
{
    /// <inheritdoc />
    /// <summary>
    ///     Manages all available Templates.
    /// </summary>
    public class TemplateManager : BindableBase
    {
        /// <summary>
        ///     Static instance of the manager.
        /// </summary>
        public static TemplateManager Manager = new TemplateManager();

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

                if (!Directory.Exists(TemplateMetadata.BaseDirectory))
                    Directory.CreateDirectory(TemplateMetadata.BaseDirectory);

                var result = new List<TemplateMetadata>();
                var files = Directory.GetFiles(TemplateMetadata.BaseDirectory);

                foreach (var file in files)
                {
                    if (!file.EndsWith(TemplateMetadata.Extension))
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