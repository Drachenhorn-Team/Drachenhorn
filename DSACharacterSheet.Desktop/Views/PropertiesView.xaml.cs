using DSACharacterSheet.Core.Lang;
using DSACharacterSheet.Desktop.Settings;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
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

namespace DSACharacterSheet.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für SettingsView.xaml
    /// </summary>
    public partial class PropertiesView : Window
    {
        public PropertiesView()
        {
            InitializeComponent();
            this.DataContext = PropertiesManager.Properties;
        }


        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                FlagImage.Source = new BitmapImage(
                    new Uri("pack://application:,,,/DSACharacterSheet.Core;component/Images/Flags/" + PropertiesManager.Properties.CurrentCulture.Name + ".png"));
            }
            catch (IOException)
            {
                FlagImage.Source = new BitmapImage(
                    new Uri("pack://application:,,,/DSACharacterSheet.Core;component/Images/Flags/invariant.png"));
            }
        }
    }
}
