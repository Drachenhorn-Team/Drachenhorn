using System;
using System.Collections.Generic;
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
using Drachenhorn.Core.Lang;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Desktop.UserElements
{
    public class LocalizedComboBox : ComboBox
    {
        static LocalizedComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LocalizedComboBox), new FrameworkPropertyMetadata(typeof(LocalizedComboBox)));
        }

        public LocalizedComboBox()
        {
            this.Style = new Style(GetType(), this.FindResource(typeof(ComboBox)) as Style);

            this.Loaded += (sender, args) =>
            {
                try
                {
                    SimpleIoc.Default.GetInstance<LanguageManager>().LanguageChanged += (s, a) =>
                    {
                        this.GetBindingExpression(ComboBox.ItemsSourceProperty)?.UpdateTarget();
                        this.GetBindingExpression(ComboBox.TextProperty)?.UpdateTarget();
                        this.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateTarget();
                        this.GetBindingExpression(ComboBox.SelectedValueProperty)?.UpdateTarget();
                    };
                }
                catch (InvalidOperationException) { }
            };
        }
    }
}
