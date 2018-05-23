using Drachenhorn.Core.Lang;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Drachenhorn.Desktop.Converter
{
    public class ValueTranslationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var val = value.ToString();

            if (val.Contains("%"))
                return LanguageManager.TextTranslate(val);

            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}