using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSACharacterSheet.Core.Printing.Exceptions
{
    public class PrintingException : Exception
    {
        #region c'tor

        public PrintingException(string message, Exception innerException = null) : base(message, innerException) { }

        #endregion c'tor
    }
}
