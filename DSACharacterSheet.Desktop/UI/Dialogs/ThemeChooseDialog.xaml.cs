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
using System.Windows.Shapes;
using DSACharacterSheet.Core.Settings;
using GalaSoft.MvvmLight.Ioc;

namespace DSACharacterSheet.Desktop.UI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für ThemeChooseDialog.xaml
    /// </summary>
    public partial class ThemeChooseDialog : Window
    {
        public ThemeChooseDialog()
        {
            InitializeComponent();
        }

        private void VisualTheme_1_OnClick(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<ISettings>().VisualTheme = VisualThemeType.White;
            this.Close();
        }

        private void VisualTheme_2_OnClick(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<ISettings>().VisualTheme = VisualThemeType.Light;
            this.Close();
        }

        private void VisualTheme_3_OnClick(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<ISettings>().VisualTheme = VisualThemeType.Dark;
            this.Close();
        }

        private void VisualTheme_4_OnClick(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<ISettings>().VisualTheme = VisualThemeType.Black;
            this.Close();
        }
    }
}
