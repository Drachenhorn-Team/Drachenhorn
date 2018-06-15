using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Drachenhorn.Desktop.Converter
{
    public class ListTypeFilterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is Type) && !(value is IEnumerable<object>))
                return null;

            var list = (IEnumerable<object>) value;

            return list?.Select(x => x.GetType() == (Type) parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
