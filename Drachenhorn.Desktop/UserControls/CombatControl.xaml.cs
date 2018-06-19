using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Drachenhorn.Xml.Sheet.CombatInfo;
using Drachenhorn.Xml.Sheet.InventoryInfo;

namespace Drachenhorn.Desktop.UserControls
{
    /// <summary>
    ///     Interaktionslogik für CombatView.xaml
    /// </summary>
    public partial class CombatControl : UserControl
    {
        private ICollectionView WeaponCollectionView;
        private ICollectionView ArmorCollectionView;

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
            eventArg.RoutedEvent = MouseWheelEvent;
            eventArg.Source = sender;
            var parent = ((Control) sender).Parent as UIElement;
            parent?.RaiseEvent(eventArg);
        }

        private void CombatControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is Inventory)
            {
                WeaponCollectionView = new CollectionViewSource() {Source = ((Inventory)this.DataContext).Items}.View;
                WeaponCollectionView.Filter = w => w is Weapon;
            }

            WeaponDataGrid.ItemsSource = WeaponCollectionView;


            if (this.DataContext is Inventory)
            {
                ArmorCollectionView = new CollectionViewSource() { Source = ((Inventory)this.DataContext).Items }.View;
                ArmorCollectionView.Filter = w => w is ArmorPart;
            }

            ArmorDataGrid.ItemsSource = ArmorCollectionView;
        }

        private void AddWeaponButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(this.DataContext is Inventory))
                return;

            ((Inventory)this.DataContext).Items.Add(new Weapon());
        }

        private void AddArmorButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(this.DataContext is Inventory))
                return;

            ((Inventory)this.DataContext).Items.Add(new ArmorPart());
        }
    }
}