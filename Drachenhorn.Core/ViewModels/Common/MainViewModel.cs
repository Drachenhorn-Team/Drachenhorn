using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Drachenhorn.Core.IO;
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.Printing;
using Drachenhorn.Core.Settings;
using Drachenhorn.Core.UI;
using Drachenhorn.Core.ViewModels.Sheet;
using Drachenhorn.Xml.Calculation;
using Drachenhorn.Xml.Exceptions;
using Drachenhorn.Xml.Template;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Core.ViewModels.Common
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties

        private ObservableCollection<CharacterSheetViewModel> _characterSheetViewModels =
            new ObservableCollection<CharacterSheetViewModel>();

        public ObservableCollection<CharacterSheetViewModel> CharacterSheetViewModels
        {
            get => _characterSheetViewModels;
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
            get => _currentSheetViewModel;
            set
            {
                if (_currentSheetViewModel == value)
                    return;
                _currentSheetViewModel = value;
                RaisePropertyChanged();
            }
        }

        public TemplateMetadata CurrentTemplate
        {
            get => SimpleIoc.Default.GetInstance<ISettings>().CurrentTemplate;
            set
            {
                if (SimpleIoc.Default.GetInstance<ISettings>().CurrentTemplate == value)
                    return;
                SimpleIoc.Default.GetInstance<ISettings>().CurrentTemplate = value;
                _currentCompleteTemplate = null;
                RaisePropertyChanged();
                RaisePropertyChanged("CurrentCompleteTemplate");
            }
        }

        private SheetTemplate _currentCompleteTemplate;
        public SheetTemplate CurrentCompleteTemplate
        {
            get
            {
                if (_currentCompleteTemplate == null && CurrentTemplate != null)
                {
                    var temp = SheetTemplate.AvailableTemplates.FirstOrDefault(CurrentTemplate.Equals);
                    if (temp != null) _currentCompleteTemplate = SheetTemplate.Load(temp.Path);
                }

                return _currentCompleteTemplate;
            }
        }

        #endregion Properties

        #region Commands

        public RelayCommand Save => new RelayCommand(ExecuteSave);

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

        public RelayCommand SaveAs => new RelayCommand(ExecuteSaveAs);

        private void ExecuteSaveAs()
        {
            CurrentSheetViewModel?.SaveAs();
        }

        public RelayCommand SaveAll => new RelayCommand(ExecuteSaveAll);

        private void ExecuteSaveAll()
        {
            foreach (var model in CharacterSheetViewModels) model.Save();
        }

        public RelayCommand Open => new RelayCommand(ExecuteOpen);

        private void ExecuteOpen()
        {
            var ioService = SimpleIoc.Default.GetInstance<IIoService>();

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

        public RelayCommand New => new RelayCommand(ExecuteNew);

        private void ExecuteNew()
        {
            var model = new CharacterSheetViewModel();
            CharacterSheetViewModels.Add(model);
            CurrentSheetViewModel = model;
        }

        public RelayCommand Print => new RelayCommand(ExecutePrint);

        private void ExecutePrint()
        {
            Messenger.Default.Send(new NotificationMessage(this, "ShowPrintView"));
        }

        public RelayCommand GeneratePDF => new RelayCommand(ExecuteGeneratePDF);

        private async void ExecuteGeneratePDF()
        {
            if (CurrentSheetViewModel?.CurrentSheet == null)
                await SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage(
                                LanguageManager.Translate("UI.NothingSelected"),
                                LanguageManager.Translate("UI.NothingSelected.Title"));

            SimpleIoc.Default.GetInstance<IUIService>().SetBusyState();

            await PrintingManager.GeneratePDFAsync(CurrentSheetViewModel.CurrentSheet);
        }

        public RelayCommand<CharacterSheetViewModel> CloseSheet =>
            new RelayCommand<CharacterSheetViewModel>(ExecuteCloseSheet);

        private async void ExecuteCloseSheet(CharacterSheetViewModel model)
        {
            if (!model.CurrentSheet.HasChanged)
            {
                CharacterSheetViewModels.Remove(model);
                return;
            }

            var result = await SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage(
                LanguageManager.Translate("UI.SouldClose"),
                LanguageManager.Translate("UI.SouldClose.Caption"),
                LanguageManager.Translate("UI.Yes"),
                LanguageManager.Translate("UI.No"), null);

            if (result)
                CharacterSheetViewModels.Remove(model);
        }

        public RelayCommand CalculateAll => new RelayCommand(ExecuteCalculateAll);

        private void ExecuteCalculateAll()
        {
            if (CurrentSheetViewModel?.CurrentSheet == null)
                return;

            Formula.RaiseCalculateAll(CurrentSheetViewModel.CurrentSheet);
        }


        public RelayCommand OpenTemplates => new RelayCommand(ExecuteOpenTemplates);

        private void ExecuteOpenTemplates()
        {
            Messenger.Default.Send(new NotificationMessage(this, "ShowOpenTemplates"));
        }

        #endregion Commands
    }
}