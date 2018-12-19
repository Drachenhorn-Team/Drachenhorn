using System;

namespace Drachenhorn.Core.Printing.Exceptions
{
    public class PrintingException : Exception
    {
        #region c'tor

        public PrintingException(string message, Exception innerException = null) : base(message, innerException)
        {
        }

        #endregion
    }
}