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
using Drachenhorn.Xml.Data.AP;

namespace Drachenhorn.Desktop.UserControls.Common
{
    /// <summary>
    /// Interaktionslogik für APTableControl.xaml
    /// </summary>
    public partial class APTableControl : UserControl
    {
        public APTableControl()
        {
            InitializeComponent();
        }

        private void AddValueButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;

            (button.DataContext as APColumn)?.Add(0);
        }

        private void AddColumnButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(this.DataContext is APTable))
                return;

            var table = (APTable) this.DataContext;

            table.APColumns.Add(new APColumn());
        }

        private void RemoveColumnButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            var table = (APTable)this.DataContext;

            table.APColumns.Remove(button.DataContext as APColumn);
        }

        private void TextBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!(sender is TextBox))
                return;

            (sender as TextBox)?.SelectAll();
        }
    }

    public class EnterKeyTraversal
    {
        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        static void ue_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var ue = e.OriginalSource as FrameworkElement;

            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private static void ue_Unloaded(object sender, RoutedEventArgs e)
        {
            var ue = sender as FrameworkElement;
            if (ue == null) return;

            ue.Unloaded -= ue_Unloaded;
            ue.PreviewKeyDown -= ue_PreviewKeyDown;
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool),

                typeof(EnterKeyTraversal), new UIPropertyMetadata(false, IsEnabledChanged));

        static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ue = d as FrameworkElement;
            if (ue == null) return;

            if ((bool)e.NewValue)
            {
                ue.Unloaded += ue_Unloaded;
                ue.PreviewKeyDown += ue_PreviewKeyDown;
            }
            else
            {
                ue.PreviewKeyDown -= ue_PreviewKeyDown;
            }
        }
    }
}
