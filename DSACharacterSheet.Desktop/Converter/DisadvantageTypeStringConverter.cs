using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using DSACharacterSheet.Xml.Sheet.Common;
using DSACharacterSheet.Xml.Sheet.Enums;

namespace DSACharacterSheet.Desktop.Converter
{
    public class DisAdvantageTypeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DisAdvantage))
                return null;

            var type = ((DisAdvantage) value).Type;

            return type == DisAdvantageType.Advantage ? "+" : "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
