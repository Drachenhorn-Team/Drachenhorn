using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Drachenhorn.Desktop.Converter
{
    public class NullVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var onNull = Visibility.Collapsed;
            var notNull = Visibility.Visible;

            if (parameter != null && parameter.ToString() == "Invert")
            {
                onNull = Visibility.Visible;
                notNull = Visibility.Collapsed;
            }

            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return onNull;

            return notNull;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
