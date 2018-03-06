using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using DSACharacterSheet.FileReader;

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

            CharacterSheetViewModels.Add(new CharacterSheetViewModel(sheet));
        }

        #endregion c'tor
    }
}
