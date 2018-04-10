using DSACharacterSheet.Core.Lang;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DSACharacterSheet.Desktop.Converter
{
    public sealed class EnumToAbbreviationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return LanguageManager.Translate(value.GetType().Name + "." + value.ToString() + ".Abbr");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string str = (string)value;

            foreach (object enumValue in Enum.GetValues(targetType))
            {
                if (str == LanguageManager.Translate(enumValue.GetType().Name + "." + enumValue.ToString() + ".Abbr"))
                { return enumValue; }
            }

            throw new ArgumentException(null, "value");
        }
    }
}
