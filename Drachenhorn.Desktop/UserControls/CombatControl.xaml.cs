using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Drachenhorn.Desktop.UserControls
{
    /// <summary>
    /// Interaktionslogik für CombatView.xaml
    /// </summary>
    public partial class CombatControl : UserControl
    {
        public CombatControl()
        {
            InitializeComponent();
        }

        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
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
    }
}