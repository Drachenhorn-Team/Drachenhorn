using System;

namespace Drachenhorn.Xml.Exceptions
{
    /// <summary>
    ///     Exception thrown when a CharacterSheet could not be saved.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class SheetSavingException : Exception
    {
        private string _path;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SheetSavingException" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="innerException">The inner exception.</param>
        public SheetSavingException(string path, Exception innerException = null) : base(
            "Error while saving File to \"" + path + "\".", innerException)
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