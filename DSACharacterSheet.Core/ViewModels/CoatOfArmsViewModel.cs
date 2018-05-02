using DSACharacterSheet.Core.IO;
using DSACharacterSheet.Core.Lang;
using DSACharacterSheet.Xml.Sheet.Common;
using GalaSoft.MvvmLight.Command;
using System;

namespace DSACharacterSheet.Core.ViewModels
{
    public class CoatOfArmsViewModel : ViewModelBase
    {
        private IIOService _ioService;

        #region c'tor

        public CoatOfArmsViewModel(IIOService ioService)
        {
            _ioService = ioService;
        }

        #endregion c'tor

        #region Commands

        private void InitializeCommands()
        {
            ImportImage = new RelayCommand(ExecuteImportImage);
            ExportImage = new RelayCommand(ExecuteExportImage);
        }

        public RelayCommand ImportImage { get; private set; }

        private void ExecuteImportImage()
        {
            if (!(Data is CoatOfArms))
                return;

            var data = _ioService.OpenDataDialog(".png",
                LanguageManager.Translate("PNG.FileType.Name"),
                LanguageManager.Translate("PNG.OpenDialog.Title"));

            ((CoatOfArms)Data).Base64String = Convert.ToBase64String(data);
        }

        public RelayCommand ExportImage { get; private set; }

        private void ExecuteExportImage()
        {
            if (!(Data is CoatOfArms))
                return;
            var data = Convert.FromBase64String(((CoatOfArms)Data).Base64String);

            _ioService.SaveDataDialog(
                LanguageManager.Translate("CoatOfArms.DefaultFileName"),
                ".png",
                LanguageManager.Translate("PNG.FileType.Name"),
                LanguageManager.Translate("PNG.SaveDialog.Title"),
                data);
        }

        #endregion Commands
    }
}