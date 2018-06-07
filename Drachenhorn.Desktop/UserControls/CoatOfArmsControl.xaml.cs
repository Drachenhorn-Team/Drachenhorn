using System;
using Drachenhorn.Desktop.Views;
using Drachenhorn.Xml.Sheet.Common;
using System.Windows;
using System.Windows.Controls;
using Drachenhorn.Core.IO;
using Drachenhorn.Core.Lang;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Desktop.UserControls
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.DataContext is CoatOfArms))
                return;

            if (!String.IsNullOrEmpty(((CoatOfArms) this.DataContext).Base64String))
            {
                if (!SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage(
                            LanguageManager.Translate("CoatOfArms.New.AreYouSure"),
                            LanguageManager.Translate("UI.New"),
                            LanguageManager.Translate("UI.Yes"),
                            LanguageManager.Translate("UI.No"),
                            null).Result)
                    return;
            }

            var view = new CoatOfArmsPainterView();

            view.Closing += (s, args) =>
            {
                ((CoatOfArms)this.DataContext).Base64String = view.GetBase64();
            };

            view.Show();
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((CoatOfArms)this.DataContext).Base64String = null;
        }

        private void ImportButton_OnClick(object sender, RoutedEventArgs e)
        {
            var data = SimpleIoc.Default.GetInstance<IIoService>().OpenDataDialog(
                ".png",
                LanguageManager.Translate("PNG.FileType.Name"),
                LanguageManager.Translate("UI.Import"));

            if (data != null)
                ((CoatOfArms)this.DataContext).Base64String = Convert.ToBase64String(data);
        }

        private void ExportButton_OnClick(object sender, RoutedEventArgs e)
        {
            var image = ((CoatOfArms)this.DataContext).Base64String;

            if (image == null) return;

            var data = Convert.FromBase64String(image);

            SimpleIoc.Default.GetInstance<IIoService>().SaveDataDialog(
                LanguageManager.Translate("CharacterSheet.CoatOfArms").Replace("/", "_"),
                ".png",
                LanguageManager.Translate("PNG.FileType.Name"),
                LanguageManager.Translate("UI.Export"),
                data);
        }
    }
}