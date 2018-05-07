using System;
using System.Collections.ObjectModel;
using DSACharacterSheet.Core.IO;
using DSACharacterSheet.Core.Lang;
using DSACharacterSheet.Core.Printing;
using DSACharacterSheet.Core.ViewModels.Sheet;
using DSACharacterSheet.Xml.Calculation;
using DSACharacterSheet.Xml.Exceptions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace DSACharacterSheet.Core.ViewModels.Common
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties

        private ObservableCollection<CharacterSheetViewModel> _characterSheetViewModels =
            new ObservableCollection<CharacterSheetViewModel>();

        public ObservableCollection<CharacterSheetViewModel> CharacterSheetViewModels
        {
            get { return _characterSheetViewModels; }
            set
            {
                if (_characterSheetViewModels == value)
                    return;
                _characterSheetViewModels = value;
                RaisePropertyChanged();
            }
        }

        private CharacterSheetViewModel _currentSheetViewModel;

        public CharacterSheetViewModel CurrentSheetViewModel
        {
            get { return _currentSheetViewModel; }
            set
            {
                if (_currentSheetViewModel == value)
                    return;
                _currentSheetViewModel = value;
                Formula.CurrentSheet = value?.CurrentSheet;
                RaisePropertyChanged();
            }
        }

        #endregion Properties

        #region c'tor

        public MainViewModel()
        {
            InitializeCommands();
        }

        #endregion c'tor

        #region Commands

        private void InitializeCommands()
        {
            Save = new RelayCommand(ExecuteSave);
            SaveAs = new RelayCommand(ExecuteSaveAs);
            SaveAll = new RelayCommand(ExecuteSaveAll);
            Open = new RelayCommand(ExecuteOpen);
            New = new RelayCommand(ExecuteNew);

            Print = new RelayCommand(ExecutePrint, () => false);

            GenerateHtml = new RelayCommand(ExecuteGenerateHtml);

            ShowSettings = new RelayCommand(ExecuteShowSettings);

            CloseSheet = new RelayCommand<CharacterSheetViewModel>(ExecuteCloseSheet);

            CalculateAll = new RelayCommand(ExecuteCalculateAll);

            OpenTemplates = new RelayCommand(ExecuteOpenTemplates);
        }

        public RelayCommand Save { get; private set; }

        private void ExecuteSave()
        {
            if (!string.IsNullOrEmpty(CurrentSheetViewModel?.CurrentSheet?.FilePath))
                try
                {
                    CurrentSheetViewModel.Save();
                }
                catch (SheetSavingException ex)
                {
                    Messenger.Default.Send<Exception>(ex);
                }
            else
                ExecuteSaveAs();
        }

        public RelayCommand SaveAs { get; private set; }

        private void ExecuteSaveAs()
        {
            CurrentSheetViewModel?.SaveAs();
        }

        public RelayCommand SaveAll { get; private set; }

        private void ExecuteSaveAll()
        {
            foreach (var model in CharacterSheetViewModels)
            {
                model.Save();
            }
        }

        public RelayCommand Open { get; private set; }

        private void ExecuteOpen()
        {
            var ioService = SimpleIoc.Default.GetInstance<IIOService>();

            try
            {
                var temp = ioService.OpenCharacterSheet();

                if (temp == null)
                    return;

                var model = new CharacterSheetViewModel(temp);
                CharacterSheetViewModels.Add(model);
                CurrentSheetViewModel = model;
            }
            catch (SheetLoadingException ex)
            {
                Messenger.Default.Send<Exception>(ex);
            }
        }

        public RelayCommand New { get; private set; }

        private void ExecuteNew()
        {
            var model = new CharacterSheetViewModel();
            CharacterSheetViewModels.Add(model);
            CurrentSheetViewModel = model;
        }

        public RelayCommand ShowSettings { get; private set; }

        private void ExecuteShowSettings()
        {
            Messenger.Default.Send(new NotificationMessage(this, "ShowSettingsView"));
        }

        public RelayCommand GenerateHtml { get; private set; }

        private void ExecuteGenerateHtml()
        {
            var ioService = SimpleIoc.Default.GetInstance<IIOService>();

            ioService.SaveStringDialog(
                string.IsNullOrEmpty(CurrentSheetViewModel.CurrentSheet.Characteristics.Name) ?
                    LanguageManager.Translate("HTML.DefaultFileName") :
                    CurrentSheetViewModel.CurrentSheet.Characteristics.Name,
                ".html",
                LanguageManager.Translate("HTML.FileType.Name"),
                LanguageManager.Translate("HTML.SaveDialog.Title"),
                PrintingManager.GenerateHtml(CurrentSheetViewModel.CurrentSheet),
                true
                );
        }

        public RelayCommand Print { get; private set; }

        private void ExecutePrint()
        {
            Messenger.Default.Send(new NotificationMessage(this, "ShowPrintView"));
        }

        public RelayCommand<CharacterSheetViewModel> CloseSheet { get; private set; }

        private void ExecuteCloseSheet(CharacterSheetViewModel model)
        {
            CharacterSheetViewModels.Remove(model);
        }

        public RelayCommand CalculateAll { get; private set; }

        private void ExecuteCalculateAll()
        {
            if (CurrentSheetViewModel?.CurrentSheet == null)
                return;

            Formula.RaiseCalculateAll(CurrentSheetViewModel.CurrentSheet);
        }


        public RelayCommand OpenTemplates { get; private set; }

        private void ExecuteOpenTemplates()
        {
            Messenger.Default.Send(new NotificationMessage(this, "ShowOpenTemplates"));
        }

        #endregion Commands
    }
}