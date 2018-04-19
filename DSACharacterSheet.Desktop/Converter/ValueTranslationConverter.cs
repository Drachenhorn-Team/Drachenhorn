using DSACharacterSheet.Core.Lang;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DSACharacterSheet.Desktop.Converter
{
    public class ValueTranslationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return LanguageManager.Translate(value?.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}