using Drachenhorn.Xml.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Drachenhorn.Desktop.Converter
{
    public class AllowedDaysConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DSAMonth))
                return null;

            var allowed = ((DSAMonth)value).AllowedDays();

            var result = new List<int>();

            for (int i = 1; i <= allowed; ++i)
                result.Add(i);

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}