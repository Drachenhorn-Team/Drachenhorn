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
    public class TemplateMetadataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SheetTemplate templ)
                return TemplateManager.Manager.AvailableTemplates.FirstOrDefault(x => x.Path == templ.Path);

            if (value is TemplateMetadata meta)
                return meta.EntireTemplate;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SheetTemplate templ)
                return TemplateManager.Manager.AvailableTemplates.FirstOrDefault(x => x.Path == templ.Path);

            if (value is TemplateMetadata meta)
                return meta.EntireTemplate;

            return null;
        }
    }
}
