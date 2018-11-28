using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Drachenhorn.Core.ViewModels.Sheet;
using Drachenhorn.Desktop.UI;
using Drachenhorn.Desktop.Views;
using Drachenhorn.Xml.Sheet.Skills;

namespace Drachenhorn.Desktop.UserControls
{
    /// <summary>
    ///     Interaktionslogik für BaseValuesControl.xaml
    /// </summary>
    public partial class BaseValuesControl : UserControl
    {
        public BaseValuesControl()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(List.ItemsSource is IList<BaseValue>))
                return;

            var newItem =
                new BaseValue((this.FindParent<CharacterSheetControl>().DataContext as CharacterSheetViewModel)
                    ?.CurrentSheet);
            new BaseValueView(newItem).ShowDialog();

            ((IList<BaseValue>) List.ItemsSource).Add(newItem);
            //List.SelectedItem = newItem;
        }

        private void List_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Handled || e.MouseDevice.Captured is ComboBox)
                return;

            e.Handled = true;
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = MouseWheelEvent;
            eventArg.Source = sender;
            var parent = ((Control)sender).Parent as UIElement;
            parent.RaiseEvent(eventArg);
        }
    }
}