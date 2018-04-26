using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DSACharacterSheet.Xml.Objects;

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
