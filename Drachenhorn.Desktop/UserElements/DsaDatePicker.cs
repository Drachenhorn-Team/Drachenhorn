using System.Windows;
using System.Windows.Controls;
using Drachenhorn.Xml.Objects;

namespace Drachenhorn.Desktop.UserElements
{
    public class DsaDatePicker : Control
    {
        public static DependencyProperty DateProperty =
            DependencyProperty.Register(
                "Date",
                typeof(DSADate),
                typeof(DsaDatePicker));

        #region c'tor

        static DsaDatePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DsaDatePicker),
                new FrameworkPropertyMetadata(typeof(DsaDatePicker)));
        }

        #endregion

        #region Properties

        public DSADate Date
        {
            get => (DSADate) GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        #endregion
    }
}