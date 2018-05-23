using Drachenhorn.Xml.Sheet.Common;
using Drachenhorn.Xml.Sheet.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Drachenhorn.Desktop.Converter
{
    public class DisAdvantageTypeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DisAdvantage))
                return null;

            var type = ((DisAdvantage)value).Type;

            return type == DisAdvantageType.Advantage ? "+" : "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}