using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Drachenhorn.Desktop.UserControls
{
    /// <summary>
    ///     Interaktionslogik für InventoryControl.xaml
    /// </summary>
    public partial class InventoryControl : UserControl
    {
        #region c'tor

        public InventoryControl()
        {
            InitializeComponent();
        }

        #endregion

        private void List_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Handled || e.MouseDevice.Captured is ComboBox)
                return;

            e.Handled = true;
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = MouseWheelEvent;
            eventArg.Source = sender;
            var parent = ((Control) sender).Parent as UIElement;
            parent.RaiseEvent(eventArg);
        }
    }
}