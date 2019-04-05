using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Drachenhorn.Xml.Template
{
    public interface ITemplateMetadata : IEquatable<ITemplateMetadata>
    {
        double Version { get; set; }
        string Name { get; set; }

        bool IsInstalled { get; }
    }

    /// <summary>
    ///     Extension Methods for ITemplateMetadata
    /// </summary>
    public static partial class Extension
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


        #region Equals
        
        internal static bool Equals(this ITemplateMetadata meta, object obj)
        {
            return obj is ITemplateMetadata metadata && meta.Equals(metadata);
        }

        internal static int GetHashCode(this ITemplateMetadata meta)
        {
            unchecked
            {
                return (meta.Version.GetHashCode() * 397) ^ (meta.Name != null ? meta.Name.GetHashCode() : 1);
            }
        }

        internal static bool Equals(this ITemplateMetadata meta, ITemplateMetadata obj)
        {
            if (obj == null) return false;

            return meta.Name == obj.Name && Math.Abs(meta.Version - obj.Version) < double.Epsilon;
        }

        #endregion Equals
    }
}
