using System.IO;
using Drachenhorn.Xml.Sheet;
using GalaSoft.MvvmLight;

namespace Drachenhorn.Core.ViewModels.Sheet
{
    public class PrintViewModel : ViewModelBase
    {
        #region Properties

        private bool _canPrint;

        private CharacterSheet _currentSheet;

        private bool _isLoading;

        private FileInfo _selectedTemplate;

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

        public bool CanPrint
        {
            get => _canPrint;
            set
            {
                if (_canPrint == value)
                    return;
                _canPrint = value;
                RaisePropertyChanged();
            }
        }

        public FileInfo SelectedTemplate
        {
            get => _selectedTemplate;
            set
            {
                if (_selectedTemplate == value)
                    return;
                _selectedTemplate = value;
                RaisePropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading == value)
                    return;
                _isLoading = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}