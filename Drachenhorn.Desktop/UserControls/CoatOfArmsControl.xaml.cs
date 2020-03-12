using System;
using System.Windows;
using System.Windows.Controls;
using Drachenhorn.Core.IO;
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.UI;
using Drachenhorn.Desktop.Views;
using Drachenhorn.Xml.Sheet.Common;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Desktop.UserControls
{
    /// <summary>
    ///     Interaktionslogik für CoatOfArmsControl.xaml
    /// </summary>
    public partial class CoatOfArmsControl : UserControl
    {
        #region c'tor

        public CoatOfArmsControl()
        {
            InitializeComponent();
        }

        #endregion

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is CoatOfArms))
                return;

            if (!string.IsNullOrEmpty(((CoatOfArms) DataContext).Base64String))
            {
                var result = MessageFactory.NewMessage()
                    .MessageTranslated("Dialog.Replace")
                    .TitleTranslated("Dialog.Replace_Caption")
                    .ButtonTranslated("Dialog.Yes", 0)
                    .ButtonTranslated("Dialog.No")
                    .ShowMessage().Result;
                if (result != 0)
                    return;
            }

            var view = new CoatOfArmsPainterView();

            view.Closing += (s, args) =>
            {
                if (view.DialogResult == true)
                    ((CoatOfArms) DataContext).Base64String = view.GetBase64();
            };

            view.ShowDialog();
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((CoatOfArms) DataContext).Base64String = null;
        }

        private void ImportButton_OnClick(object sender, RoutedEventArgs e)
        {
            var data = SimpleIoc.Default.GetInstance<IIoService>().OpenDataDialog(
                ".png",
                LanguageManager.Translate("PNG.FileType.Name"),
                LanguageManager.Translate("UI.Import"));

            if (data != null)
                ((CoatOfArms) DataContext).Base64String = Convert.ToBase64String(data);
        }

        private void ExportButton_OnClick(object sender, RoutedEventArgs e)
        {
            var image = ((CoatOfArms) DataContext).Base64String;

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