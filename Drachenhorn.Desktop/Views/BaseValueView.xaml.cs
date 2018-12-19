using Drachenhorn.Xml.Sheet.Skills;

namespace Drachenhorn.Desktop.Views
{
    /// <summary>
    ///     Interaktionslogik für BaseValueView.xaml
    /// </summary>
    public partial class BaseValueView
    {
        #region c'tor

        public BaseValueView(BaseValue baseValue)
        {
            BaseValue = baseValue;

            DataContext = baseValue;

            InitializeComponent();
        }

        #endregion

        #region Properties

        private BaseValue _baseValue;

        public BaseValue BaseValue
        {
            get => _baseValue;
            private set
            {
                if (_baseValue == value)
                    return;
                _baseValue = value;
            }
        }

        #endregion
    }
}