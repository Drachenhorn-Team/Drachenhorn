using DSACharacterSheet.Core.ViewModel;
using DSACharacterSheet.FileReader;
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
        }

        #endregion c'tor


        #region Commands



        #endregion Commands
    }
}
