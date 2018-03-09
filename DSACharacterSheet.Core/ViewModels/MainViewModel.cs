using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using DSACharacterSheet.Core.IO;
using DSACharacterSheet.FileReader;
using DSACharacterSheet.FileReader.Exceptions;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace DSACharacterSheet.Core.ViewModels
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
                RaisePropertyChanged();
            }
        }

        #endregion Properties


        #region c'tor

        public MainViewModel()
        {
            CharacterSheet sheet = null;

            try
            {
                sheet = ServiceLocator.Current.GetInstance<CharacterSheet>("InitialSheet");
            }
            catch (Exception)
            {
                sheet = new CharacterSheet();
            }

            sheet.Name = "test";

            CharacterSheetViewModels.Add(new CharacterSheetViewModel(sheet));

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
        }

        public RelayCommand Save { get; private set; }

        private void ExecuteSave()
        {
            if (!string.IsNullOrEmpty(CurrentSheetViewModel.CurrentSheet.FilePath))
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
            CurrentSheetViewModel.SaveAs();
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
            var ioService = ServiceLocator.Current.GetInstance<IIOService>();

            try
            {
                var temp = ioService.OpenCharacterSheet();

                if (temp != null)
                    CharacterSheetViewModels.Add(new CharacterSheetViewModel(temp));
            }
            catch (SheetLoadingException ex)
            {
                Messenger.Default.Send<Exception>(ex);
            }
        }

        public RelayCommand New { get; private set; }

        private void ExecuteNew()
        {
            CharacterSheetViewModels.Add(new CharacterSheetViewModel());
        }

        #endregion Commands
    }
}