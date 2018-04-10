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
using DSACharacterSheet.Desktop.Views;
using DSACharacterSheet.FileReader.Sheet.Skills;
using Attribute = DSACharacterSheet.FileReader.Sheet.Skills.Attribute;

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

            ((IList<Attribute>)List.ItemsSource).Add(newItem);
            List.SelectedItem = newItem;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(List.ItemsSource is IList<Attribute>))
                return;

            ((IList<Attribute>)List.ItemsSource).Remove((Attribute)List.SelectedItem);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListViewColumns(sender, e);
        }

        private void UpdateListViewColumns(object sender, EventArgs args)
        {
            GridView gridView = List.View as GridView;

            if (gridView != null)
                foreach (GridViewColumn column in gridView.Columns)
                {
                    column.Width = column.ActualWidth;
                    column.Width = double.NaN;
                }
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
