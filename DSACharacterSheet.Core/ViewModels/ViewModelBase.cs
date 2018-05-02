namespace DSACharacterSheet.Core.ViewModels
{
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        private object _data;

        public object Data
        {
            get { return _data; }
            set
            {
                if (_data == value)
                    return;
                _data = value;
                RaisePropertyChanged();
            }
        }
    }
}