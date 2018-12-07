using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Drachenhorn.Desktop.Converter.Validation
{
    public class XmlStringValidation : ValidationRule
    {
        private static readonly List<char> forbiddenChars = new List<char>()
        {
            '&'
        };


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string))
                return new ValidationResult(false, null);

            var str = (string) value;

            foreach (var forbiddenChar in forbiddenChars)
            {
                if (str.Contains(forbiddenChar))
                    return new ValidationResult(false, "'" + forbiddenChar + "' not allowed");
            }

            return new ValidationResult(true, null);
        }
    }
}
