using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Drachenhorn.Xml.Data.AP;

namespace Drachenhorn.Desktop.UserControls.Common
{
    /// <summary>
    ///     Interaktionslogik für APTableControl.xaml
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

        private void RemoveValueButton_OnClick(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement) sender;

            if (!(element.DataContext is APValue))
                return;

            if (!(element.Tag is IList<APValue>))
                return;

            var tag = (IList<APValue>) element.Tag;

            tag.Remove((APValue) element.DataContext);
        }

        private void AddColumnButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is APTable))
                return;

            var table = (APTable) DataContext;

            table.APColumns.Add(new APColumn());
        }

        private void RemoveColumnButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;

            var table = (APTable) DataContext;

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
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool),
                typeof(EnterKeyTraversal), new UIPropertyMetadata(false, IsEnabledChanged));

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool) obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        private static void ue_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ue = d as FrameworkElement;
            if (ue == null) return;

            if ((bool) e.NewValue)
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