using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Drachenhorn.Xml.Sheet.Skills;

namespace Drachenhorn.Desktop.Converter
{
    public class SkillCategoryConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IEnumerable<Skill>))
                return null;

            var val = (IEnumerable<Skill>) value;

            return (from x in val select x.Category).Distinct();
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}