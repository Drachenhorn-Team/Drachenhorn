using DSACharacterSheet.FileReader.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DSACharacterSheet.Desktop.UserControls
{
    /// <summary>
    /// Interaktionslogik für SpecialSkillControl.xaml
    /// </summary>
    public partial class SpecialSkillControl : UserControl
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
    }
}
