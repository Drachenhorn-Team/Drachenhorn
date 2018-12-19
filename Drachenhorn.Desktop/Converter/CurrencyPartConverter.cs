using System;
using System.Globalization;
using System.Windows.Data;
using Drachenhorn.Xml.Objects;

namespace Drachenhorn.Desktop.Converter
{
    public class CurrencyPartConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is int) || !(values[1] is CurrencyPart))
                return 0;

            var val = (int) values[0];
            var curr = (CurrencyPart) values[1];

            return curr?.Convert(val).ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}