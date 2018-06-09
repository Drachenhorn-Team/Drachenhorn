using System;
using Drachenhorn.Core.IO;
using Drachenhorn.Core.Lang;
using Drachenhorn.Xml.Exceptions;
using Drachenhorn.Xml.Sheet;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace Drachenhorn.Core.ViewModels.Sheet
{
    public class CharacterSheetViewModel : ViewModelBase
    {
        #region Properties

        private CharacterSheet _currentSheet = new CharacterSheet();

        public CharacterSheet CurrentSheet
        {
            get { return _currentSheet; }
            set
            {
                if (_currentSheet == value)
                    return;
                _currentSheet = value;
                RaisePropertyChanged();
            }
        }

        #endregion Properties

        #region c'tor

        public CharacterSheetViewModel()
        {
        }

        public CharacterSheetViewModel(CharacterSheet sheet)
        {
            CurrentSheet = sheet;
        }

        #endregion c'tor

        #region Actions

        public void Save(string path = null)
        {
            if (path == null)
                CurrentSheet.Save();
            else
                CurrentSheet.Save(path);
        }

        public void SaveAs()
        {
            var ioService = SimpleIoc.Default.GetInstance<IIoService>();

            try
            {
                ioService.SaveAs(CurrentSheet,
                string.IsNullOrEmpty(CurrentSheet.Characteristics.Name)
                    ? LanguageManager.Translate("CharacterSheet.SaveDialog.DefaultFileName")
                    : CurrentSheet.Characteristics.Name,
                CharacterSheet.Extension,
                LanguageManager.Translate("CharacterSheet.FileType.Name"),
                LanguageManager.Translate("CharacterSheet.SaveDialog.Title"));
            }
            catch (SheetSavingException ex)
            {
                Messenger.Default.Send<Exception>(ex);
            }
        }

        public void Open()
        {
            var ioService = SimpleIoc.Default.GetInstance<IIoService>();

            try
            {
                ioService.OpenCharacterSheet();
            }
            catch (SheetLoadingException ex)
            {
                Messenger.Default.Send<Exception>(ex);
            }
        }

        #endregion Actions
    }
}