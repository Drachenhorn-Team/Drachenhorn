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

            TypeDescriptor.GetProperties(List)["ItemsSource"]
                .AddValueChanged(List, UpdateList);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(List.ItemsSource is IList<Skill>))
                return;

            var newItem = new Skill();

            ((IList<Skill>) List.ItemsSource).Add(newItem);
            List.SelectedItem = newItem;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(List.ItemsSource is IList<Skill>))
                return;

            ((IList<Skill>) List.ItemsSource).Remove((Skill) List.SelectedItem);
        }

        private void UpdateList(object sender, EventArgs e)
        {
            if (List.ItemsSource == null) return;

            var view = (CollectionView) CollectionViewSource.GetDefaultView(List.ItemsSource);
            view.GroupDescriptions.Clear();
            var groupDescription = new PropertyGroupDescription("Category");
            view.GroupDescriptions.Add(groupDescription);

            UpdateListViewColumns(sender, e);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListViewColumns(sender, e);
        }

        private void CategoryBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList(sender, e);
        }

        private void UpdateListViewColumns(object sender, EventArgs args)
        {
            var gridView = List.View as GridView;

            if (gridView != null)
                foreach (var column in gridView.Columns)
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
            eventArg.RoutedEvent = MouseWheelEvent;
            eventArg.Source = sender;
            var parent = ((Control) sender).Parent as UIElement;
            parent.RaiseEvent(eventArg);
        }
    }
}