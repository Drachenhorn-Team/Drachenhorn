using DSACharacterSheet.Xml.Sheet.Skills;
using System.Windows;

namespace DSACharacterSheet.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für BaseValueView.xaml
    /// </summary>
    public partial class BaseValueView : Window
    {
        private BaseValue _baseValue;

        public BaseValue BaseValue
        {
            get { return _baseValue; }
            private set
            {
                if (_baseValue == value)
                    return;
                _baseValue = value;
            }
        }

        public BaseValueView(BaseValue baseValue)
        {
            BaseValue = baseValue;

            this.DataContext = baseValue;

            InitializeComponent();
        }
    }
}