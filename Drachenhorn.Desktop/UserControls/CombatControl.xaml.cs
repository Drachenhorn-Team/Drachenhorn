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
        #region c'tor

        public CombatControl()
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
            parent?.RaiseEvent(eventArg);
        }

        private void CombatControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is Inventory)
            {
                _weaponCollectionView = new CollectionViewSource {Source = ((Inventory) DataContext).Items}.View;
                _weaponCollectionView.Filter = w => w is Weapon;
            }

            WeaponDataGrid.ItemsSource = _weaponCollectionView;


            if (DataContext is Inventory)
            {
                _armorCollectionView = new CollectionViewSource {Source = ((Inventory) DataContext).Items}.View;
                _armorCollectionView.Filter = w => w is ArmorPart;
            }

            ArmorDataGrid.ItemsSource = _armorCollectionView;
        }

        private void AddWeaponButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is Inventory))
                return;

            ((Inventory) DataContext).Items.Add(new Weapon());
        }

        private void AddArmorButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is Inventory))
                return;

            ((Inventory) DataContext).Items.Add(new ArmorPart());
        }

        #region Properties

        private ICollectionView _armorCollectionView;
        private ICollectionView _weaponCollectionView;

        #endregion
    }
}