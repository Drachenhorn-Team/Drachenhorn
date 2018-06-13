using System.IO;
using Drachenhorn.Xml.Sheet;
using GalaSoft.MvvmLight;

namespace Drachenhorn.Core.ViewModels.Sheet
{
    public class PrintViewModel : ViewModelBase
    {
        #region Properties

        private CharacterSheet _currentSheet;
        public CharacterSheet CurrentSheet
        {
            get { return _currentSheet; }
            set
            {
                if (_currentSheet == value)
                    return;
                _currentSheet = value;
                RaisePropertyChanged();
            }
        }

        private bool _canPrint;
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

        private FileInfo _selectedTemplate;
        public FileInfo SelectedTemplate
        {
            get { return _selectedTemplate; }
            set
            {
                if (_selectedTemplate == value)
                    return;
                _selectedTemplate = value;
                RaisePropertyChanged();
            }
        }

        #endregion Properties
    }
}