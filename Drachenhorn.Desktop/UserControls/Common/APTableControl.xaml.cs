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
using Drachenhorn.Xml.Data.AP;

namespace Drachenhorn.Desktop.UserControls.Common
{
    /// <summary>
    /// Interaktionslogik für APTableControl.xaml
    /// </summary>
    public partial class APTableControl : UserControl
    {
        public APTableControl()
        {
            InitializeComponent();
        }

        private void AddValueButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;

            (button.Tag as APColumn)?.Costs.Add(0);
        }

        private void AddColumnButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(this.DataContext is APTable))
                return;

            var table = (APTable) this.DataContext;

            table.APColumns.Add(new APColumn());
        }
    }
}
