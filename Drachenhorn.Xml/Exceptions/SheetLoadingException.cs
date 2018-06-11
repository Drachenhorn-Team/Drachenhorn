using System;

namespace Drachenhorn.Xml.Exceptions
{
    /// <summary>
    ///     Exception thrown when a CharacterSheet could not be loaded.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class SheetLoadingException : Exception
    {
        private string _path;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SheetLoadingException" /> class.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="innerException">The inner exception.</param>
        public SheetLoadingException(string path, Exception innerException = null) : base(
            "File \"" + path + "\" could not be loaded.", innerException)
        {
            Path = path;
        }

        /// <summary>
        ///     Gets the path of the file.
        /// </summary>
        /// <value>
        ///     The path of the file.
        /// </value>
        public string Path
        {
            get => _path;
            private set
            {
                if (_path == value)
                    return;
                _path = value;
            }
        }
    }
}