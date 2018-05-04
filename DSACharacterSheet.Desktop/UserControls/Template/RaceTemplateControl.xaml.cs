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
using DSACharacterSheet.Xml.Sheet.Common;

namespace DSACharacterSheet.Desktop.UserControls.Template
{
    /// <summary>
    /// Interaktionslogik für RaceTemplateControl.xaml
    /// </summary>
    public partial class RaceTemplateControl : UserControl
    {
        public RaceTemplateControl()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(RaceList.ItemsSource is IList<RaceInformation>))
                return;

            ((IList<RaceInformation>)RaceList.ItemsSource).Add(new RaceInformation());
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button))
                return;

            var button = (Button)sender;

            if (!(button.DataContext is RaceInformation))
                return;

            (RaceList.ItemsSource as IList<RaceInformation>)?.Remove((RaceInformation)button.DataContext);
        }
    }
}
