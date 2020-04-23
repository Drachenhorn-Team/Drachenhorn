using System;
using System.IO;

namespace Drachenhorn.Xml.Data
{
    /// <summary>
    ///     Constants for Drachenhorn
    /// </summary>
    public static class Constants
    {
        /// <summary>
        ///     CharacterSheet File Extension
        /// </summary>
        public static readonly string SheetExtension = ".dsac";

        /// <summary>
        ///     Template File Extension
        /// </summary>
        public static readonly string TemplateExtension = ".dsat";

        /// <summary>
        ///     Template Base Directory.
        /// </summary>
        public static readonly string TemplateBaseDirectory =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Drachenhorn", "Templates");
    }
}