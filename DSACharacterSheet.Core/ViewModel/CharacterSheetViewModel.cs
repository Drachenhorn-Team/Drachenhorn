using DSACharacterSheet.Core.Interfaces;
using DSACharacterSheet.FileReader;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSACharacterSheet.Core.ViewModel
{
    public class CharacterSheetViewModel : ViewModelBase
    {
        private CharacterSheet _currentSheet = new CharacterSheet();
        public CharacterSheet CurrentSheet
        {
            get { return _currentSheet; }
            set
            {
                if (_currentSheet == value)
                    return;
                _currentSheet = value;
                OnPropertyChanged();
            }
        }

        #region c'tor

        public CharacterSheetViewModel()
        {
            _saveCommand = new DelegateCommand<IIOService>(SaveAction, CanExecuteSaveAction);
            _saveAsCommand = new DelegateCommand<IIOService>(SaveAsAction);
            _loadCommand = new DelegateCommand<IIOService>(LoadAction);
        }

        #endregion c'tor


        #region Commands

        #region Save

        private DelegateCommand<IIOService> _saveCommand;
        public DelegateCommand<IIOService> SaveCommand
        {
            get { return _saveCommand; }
        }

        public void SaveAction(IIOService ioService)
        {
            if (ioService.CanGetFileStream)
                CurrentSheet.Save(ioService.GetStream());
        }

        public bool CanExecuteSaveAction(IIOService ioService)
        {
            return !String.IsNullOrEmpty(CurrentSheet.FilePath);
        }

        private DelegateCommand<IIOService> _saveAsCommand;
        public DelegateCommand<IIOService> SaveAsCommand
        {
            get { return _saveAsCommand; }
        }

        public void SaveAsAction(IIOService ioService)
        {
            if (ioService.CanGetFileStream)
                CurrentSheet.Save(ioService.GetStream());
        }

        #endregion Save


        #region Load

        private DelegateCommand<IIOService> _loadCommand;
        public DelegateCommand<IIOService> LoadCommand
        {
            get { return _loadCommand; }
        }

        public void LoadAction(IIOService ioService)
        {
            if (ioService.CanGetFileStream)
                CurrentSheet = CharacterSheet.Load(ioService.GetStream());
        }

        #endregion Load

        #endregion Commands
    }
}
