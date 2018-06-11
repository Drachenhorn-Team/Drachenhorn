using System;
using System.Windows;
using System.Windows.Controls;
using Drachenhorn.Core.Lang;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Desktop.UserElements
{
    public class LocalizedComboBox : ComboBox
    {
        static LocalizedComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LocalizedComboBox),
                new FrameworkPropertyMetadata(typeof(LocalizedComboBox)));
        }

        public LocalizedComboBox()
        {
            Style = new Style(GetType(), FindResource(typeof(ComboBox)) as Style);

            Loaded += (sender, args) =>
            {
                try
                {
                    SimpleIoc.Default.GetInstance<LanguageManager>().LanguageChanged += (s, a) =>
                    {
                        GetBindingExpression(ItemsSourceProperty)?.UpdateTarget();
                        GetBindingExpression(TextProperty)?.UpdateTarget();
                        GetBindingExpression(SelectedItemProperty)?.UpdateTarget();
                        GetBindingExpression(SelectedValueProperty)?.UpdateTarget();
                    };
                }
                catch (InvalidOperationException)
                {
                }
            };
        }
    }
}