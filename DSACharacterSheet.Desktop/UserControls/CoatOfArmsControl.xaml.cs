using DSACharacterSheet.Desktop.Views;
using DSACharacterSheet.FileReader.Common;
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
    /// Interaktionslogik für CoatOfArmsControl.xaml
    /// </summary>
    public partial class CoatOfArmsControl : UserControl
    {
        public CoatOfArmsControl()
        {
            InitializeComponent();
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            T parent = VisualTreeHelper.GetParent(child) as T;

            if (parent != null)
                return parent;
            else
                return FindParent<T>(parent);

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.DataContext is CoatOfArms))
                return;

            var view = new CoatOfArmsPainterView(((CoatOfArms)this.DataContext).Strokes);
            view.Show();
        }
    }
}
