using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Drachenhorn.Xml.Data;

namespace Drachenhorn.Xml.Template
{
    /// <summary>
    ///     Basic Template Data
    /// </summary>
    public interface ITemplateMetadata : IEquatable<ITemplateMetadata>, INotifyPropertyChanged
    {
        /// <summary>
        ///     Path to the Template
        /// </summary>
        string Path { get; }

        /// <summary>
        ///     Version of the Template
        /// </summary>
        double Version { get; set; }

        /// <summary>
        ///     Name of the Template
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     True if Template is installed
        /// </summary>
        bool IsInstalled { get; }

        /// <summary>
        ///     Install the current Template
        /// </summary>
        /// <returns>True if successful</returns>
        bool Install();

        /// <summary>
        ///     Install the current Template (async)
        /// </summary>
        /// <returns>True if successful</returns>
        Task<bool> InstallAsync();
    }

    /// <summary>
    ///     Extension Methods for ITemplateMetadata
    /// </summary>
    public static class Extension
    {
        internal static void SetVersionAndNameFromXmlLine(this ITemplateMetadata meta, string line)
        {
            var versionMatch = new Regex("Version=\"[0-9]+[.]?[0-9]*\"").Match(line).Value;

            if (!string.IsNullOrEmpty(versionMatch))
                meta.Version = double.Parse(versionMatch.Substring(9, versionMatch.Length - 10),
                    CultureInfo.InvariantCulture);

            var nameMatch = new Regex("Name=\"[^\"]*\"").Match(line).Value;

            meta.Name = !string.IsNullOrEmpty(nameMatch) ? nameMatch.Substring(6, nameMatch.Length - 7) : "unnamed";
        }

        internal static bool CheckInstalled(this ITemplateMetadata meta)
        {
            return meta.Path != null && meta.Path.StartsWith(Constants.TemplateBaseDirectory);
        }


        #region ToString

        /// <summary>
        ///     Extension Method for getting Template as String
        /// </summary>
        /// <param name="meta">Template</param>
        /// <returns>Template-String</returns>
        public static string ToString(this ITemplateMetadata meta)
        {
            return meta.Name + " v" + meta.Version.ToString(CultureInfo.InvariantCulture) + " " + meta.Path;
        }

        #endregion ToString
    }
}