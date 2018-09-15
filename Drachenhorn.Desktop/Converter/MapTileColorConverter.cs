using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Drachenhorn.Map.Common;

namespace Drachenhorn.Desktop.Converter
{
    public class MapTileColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TileType))
                return null;

            switch ((TileType)value)
            {
                case TileType.Floor:
                    return new SolidColorBrush(Colors.White);
                case TileType.Wall:
                    return new SolidColorBrush(Colors.Black);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
