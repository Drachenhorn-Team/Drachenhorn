using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Drachenhorn.Xml.Sheet.Skills;

namespace Drachenhorn.Desktop.UserControls
{
    /// <summary>
    ///     Interaktionslogik für SkillListControl.xaml
    /// </summary>
    public partial class SkillListControl : UserControl
    {
        public SkillListControl()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var source = DataGrid.Resources["SkillViewSource"] as CollectionViewSource;
            if (!(source?.Source is IList<Skill>))
                return;

            var newItem = new Skill();

            ((IList<Skill>)source?.Source).Add(newItem);
            List.SelectedItem = newItem;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var source = DataGrid.Resources["SkillViewSource"] as CollectionViewSource;

            (source?.Source as IList<Skill>)?.Remove((Skill) List.SelectedItem);
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