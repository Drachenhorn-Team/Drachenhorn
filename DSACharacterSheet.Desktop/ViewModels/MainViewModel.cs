using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using DSACharacterSheet.Desktop.Dialogs;
using DSACharacterSheet.FileReader;
using DSACharacterSheet.FileReader.Exceptions;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;

namespace DSACharacterSheet.Desktop.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties

        private ObservableCollection<CharacterSheetViewModel> _characterSheetViewModels = new ObservableCollection<CharacterSheetViewModel>();
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
        }

        #endregion c'tor


        #region Commands

        private void InitializeCommands()
        {
            Save = new RelayCommand(ExecuteSave);
            SaveAs = new RelayCommand(ExecuteSaveAs);
        }

        public RelayCommand Save { get; private set; }

        private void ExecuteSave()
        {
            if (!string.IsNullOrEmpty(CurrentSheetViewModel.CurrentSheet.FilePath))
                try
                {
                    CurrentSheetViewModel.CurrentSheet.Save();
                }
                catch (SheetSavingException ex)
                {
                    Messenger.Default.Send<Exception>(ex);
                    //new ExceptionMessageBox(ex, ex.Message).ShowDialog();
                }
            else
                ExecuteSaveAs();
        }

        public RelayCommand SaveAs { get; private set; }

        private void ExecuteSaveAs()
        {
            var fileDialog = new SaveFileDialog
            {
                FileName = string.IsNullOrEmpty(CurrentSheetViewModel.CurrentSheet.Name) ? "Charakterbogen" : CurrentSheetViewModel.CurrentSheet.Name,
                Filter = "DSA-Charakterbogen (*.dsac)|*.dsac",
                FilterIndex = 1,
                AddExtension = true,
                Title = "Charakterbogen speichern"
            };

            if (fileDialog.ShowDialog() != true) return;

            try
            {
                CurrentSheetViewModel.CurrentSheet.Save(fileDialog.FileName);
            }
            catch (SheetSavingException ex)
            {
                new ExceptionMessageBox(ex, ex.Message).ShowDialog();
            }
        }

        #endregion Commands
    }
}
