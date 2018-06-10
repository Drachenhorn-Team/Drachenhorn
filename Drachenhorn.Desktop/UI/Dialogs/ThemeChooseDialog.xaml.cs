using System.Windows;
using Drachenhorn.Core.Settings;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    ///     Interaktionslogik für ThemeChooseDialog.xaml
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
            Close();
        }

        private void VisualTheme_2_OnClick(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<ISettings>().VisualTheme = VisualThemeType.Light;
            Close();
        }

        private void VisualTheme_3_OnClick(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<ISettings>().VisualTheme = VisualThemeType.Dark;
            Close();
        }

        private void VisualTheme_4_OnClick(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<ISettings>().VisualTheme = VisualThemeType.Black;
            Close();
        }

        private void VisualTheme_System_OnClick(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<ISettings>().VisualTheme = VisualThemeType.System;
            Close();
        }
    }
}