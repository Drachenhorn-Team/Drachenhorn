using System;
using System.Collections.Generic;
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
using Drachenhorn.Core.Settings;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Desktop.UserControls.Common
{
    /// <summary>
    /// Interaktionslogik für SettingsViewControl.xaml
    /// </summary>
    public partial class SettingsViewControl : UserControl
    {
        public SettingsViewControl()
        {
            InitializeComponent();

            Loaded += (sender, args) => { LanguageComboBox_SelectionChanged(sender, null); };
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FlagImage == null) return;

            try
            {
                var culture = SimpleIoc.Default.GetInstance<ISettings>().CurrentCulture.Name;

                var path = "pack://application:,,,/Images/Flags/" + culture + ".png";
                FlagImage.Source = new BitmapImage(new Uri(path));
            }
            catch (IOException)
            {
                var path = "pack://application:,,,/Images/Flags/invariant.png";
                FlagImage.Source = new BitmapImage(new Uri(path));
            }
        }
    }
}
