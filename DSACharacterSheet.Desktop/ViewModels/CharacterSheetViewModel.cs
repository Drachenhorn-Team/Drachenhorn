using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSACharacterSheet.FileReader;
using GalaSoft.MvvmLight;

namespace DSACharacterSheet.Desktop.ViewModels
{
    public class CharacterSheetViewModel : ViewModelBase
    {
        #region Properties

        private CharacterSheet _currentSheet = new CharacterSheet();
        public CharacterSheet CurrentSheet
        {
            get => _currentSheet;
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

        public CharacterSheetViewModel() { }

        public CharacterSheetViewModel(CharacterSheet sheet)
        {
            CurrentSheet = sheet;
        }

        #endregion c'tor
    }
}
