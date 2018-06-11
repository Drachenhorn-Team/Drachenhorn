using System;
using System.Globalization;
using System.Windows.Data;
using Drachenhorn.Core.Lang;

namespace Drachenhorn.Desktop.Converter
{
    public sealed class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return LanguageManager.Translate(value.GetType().Name + "." + value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = (string) value;

            foreach (var enumValue in Enum.GetValues(targetType))
                if (str == LanguageManager.Translate(enumValue.GetType().Name + "." + enumValue))
                    return enumValue;

            return null;
        }
    }
}