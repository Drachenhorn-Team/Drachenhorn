using System;
using System.Globalization;
using System.Windows.Data;
using Drachenhorn.Xml.Sheet.Enums;

namespace Drachenhorn.Desktop.Converter
{
    public class DisAdvantageTypeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DisAdvantageType))
                return null;

            var type = (DisAdvantageType) value;

            return type == DisAdvantageType.Advantage ? "+" : "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}