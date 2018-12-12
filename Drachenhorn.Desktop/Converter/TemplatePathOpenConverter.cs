using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Drachenhorn.Xml.Template;

namespace Drachenhorn.Desktop.Converter
{
    public class TemplatePathOpenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string) || !(parameter is TemplateManager))
                return null;

            var val = (string) value;
            var manager = (TemplateManager) parameter;

            return manager.GetTemplate(val);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var templ = value as TemplateMetadata;

            return templ?.Path;
        }
    }
}
