using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Drachenhorn.Xml.Objects;

namespace Drachenhorn.Desktop.UserControls.Template
{
    /// <summary>
    ///     Interaktionslogik für CurrenciesControl.xaml
    /// </summary>
    public partial class CurrenciesControl : UserControl
    {
        #region c'tor

        public CurrenciesControl()
        {
            InitializeComponent();
        }

        #endregion

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(CurrencyList.DataContext is IList<Currency>))
                return;

            var list = (IList<Currency>) CurrencyList.DataContext;

            list.Add(new Currency());

            ScrollViewer.ScrollToBottom();
        }

        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Control))
                return;

            if (!(CurrencyList.DataContext is IList<Currency>))
                return;

            var list = (IList<Currency>) CurrencyList.DataContext;

            var control = (Control) sender;

            if (!(control.DataContext is Currency))
                return;

            list.Remove(control.DataContext as Currency);
        }

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