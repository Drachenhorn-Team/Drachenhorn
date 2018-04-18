using System;

namespace DSACharacterSheet.Xml.Exceptions
{
    public class SheetLoadingException : Exception
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

        public SheetLoadingException(string path, Exception innerException = null) : base("File \"" + path + "\" could not be loaded.", innerException)
        {
            Path = path;
        }
    }
}