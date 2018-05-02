using DSACharacterSheet.Xml.Sheet.Skills;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DSACharacterSheet.Desktop.UserControls
{
    /// <summary>
    /// Interaktionslogik für SpecialSkillControl.xaml
    /// </summary>
    public partial class SpecialSkillControl : UserControlBase
    {
        public SpecialSkillControl()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(List.ItemsSource is IList<SpecialSkill>))
                return;

            var newItem = new SpecialSkill();

            ((IList<SpecialSkill>)List.ItemsSource).Add(newItem);
            List.SelectedItem = newItem;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(List.ItemsSource is IList<SpecialSkill>))
                return;

            ((IList<SpecialSkill>)List.ItemsSource).Remove((SpecialSkill)List.SelectedItem);
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
    }
}