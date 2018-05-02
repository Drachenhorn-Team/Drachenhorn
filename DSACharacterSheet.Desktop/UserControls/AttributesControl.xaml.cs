using DSACharacterSheet.Desktop.Views;
using DSACharacterSheet.Xml.Sheet.Skills;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DSACharacterSheet.Desktop.UserControls
{
    /// <summary>
    /// Interaktionslogik für AttributeControl.xaml
    /// </summary>
    public partial class AttributesControl : UserControl
    {
        public AttributesControl()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(List.ItemsSource is IList<Attribute>))
                return;

            var newItem = new Attribute();
            new AttributeView(newItem).ShowDialog();

            ((IList<Attribute>)List.ItemsSource).Add(newItem);
            //List.SelectedItem = newItem;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button))
                return;

            var button = (Button)sender;

            if (!(button.DataContext is Attribute))
                return;

            (List.ItemsSource as IList<Attribute>)?.Remove((Attribute)button.DataContext);
        }

        private void List_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Handled || e.MouseDevice.Captured is ComboBox)
                return;

            e.Handled = true;
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;
            var parent = ((Control)sender).Parent as UIElement;
            parent.RaiseEvent(eventArg);
        }

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var attr = (sender as FrameworkElement)?.DataContext as Attribute;

            if (attr != null)
                new AttributeView(attr).ShowDialog();
        }
    }
}