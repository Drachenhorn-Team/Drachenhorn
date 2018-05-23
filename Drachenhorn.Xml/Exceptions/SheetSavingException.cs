using System;

namespace Drachenhorn.Xml.Exceptions
{
    public class SheetSavingException : Exception
    {
        private string _path;

        public string Path
        {
            get { return _path; }
            private set
            {
                if (_path == value)
                    return;
                _path = value;
            }
        }

        public SheetSavingException(string path, Exception innerException = null) : base("Error while saving File to \"" + path + "\".", innerException)
        {
        }
    }
}