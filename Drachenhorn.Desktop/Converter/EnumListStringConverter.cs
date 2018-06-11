using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using Drachenhorn.Core.Lang;

namespace Drachenhorn.Desktop.Converter
{
    public sealed class EnumListStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var values = (Array) value;
            var result = new List<string>();

            foreach (var val in values)
                result.Add(LanguageManager.Translate(val.GetType().Name + "." + val));

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}