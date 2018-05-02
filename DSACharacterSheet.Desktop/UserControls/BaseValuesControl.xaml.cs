using DSACharacterSheet.Desktop.Views;
using DSACharacterSheet.Xml.Sheet.Skills;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DSACharacterSheet.Desktop.UserControls
{
    /// <summary>
    /// Interaktionslogik für BaseValuesControl.xaml
    /// </summary>
    public partial class BaseValuesControl : UserControlBase
    {
        public BaseValuesControl()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(List.ItemsSource is IList<BaseValue>))
                return;

            var newItem = new BaseValue();
            new BaseValueView(newItem).ShowDialog();

            ((IList<BaseValue>)List.ItemsSource).Add(newItem);
            //List.SelectedItem = newItem;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button))
                return;

            var button = (Button)sender;

            if (!(button.DataContext is BaseValue))
                return;

            (List.ItemsSource as IList<BaseValue>)?.Remove((BaseValue)button.DataContext);
        }
    }
}