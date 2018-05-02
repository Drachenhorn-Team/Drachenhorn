using DSACharacterSheet.Xml.Objects;
using System.Windows;
using System.Windows.Controls;

namespace DSACharacterSheet.Desktop.UserElements
{
    public class DsaDatePicker : Control
    {
        #region Properties

        public static DependencyProperty DateProperty =
            DependencyProperty.Register(
                "Date",
                typeof(DSADate),
                typeof(DsaDatePicker));

        public DSADate Date
        {
            get { return (DSADate)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        #endregion Properties

        static DsaDatePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DsaDatePicker), new FrameworkPropertyMetadata(typeof(DsaDatePicker)));
        }
    }
}