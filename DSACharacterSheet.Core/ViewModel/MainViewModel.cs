using DSACharacterSheet.Core.Printing;
using DSACharacterSheet.Core.ViewModel;
using DSACharacterSheet.FileReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DSACharacterSheet.Core.ViewModel
{
    public class MainViewModel : ViewModelBase
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
    }
}
