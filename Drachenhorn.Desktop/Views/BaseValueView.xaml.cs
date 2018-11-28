using System.Windows;
using Drachenhorn.Xml.Sheet.Skills;

namespace Drachenhorn.Desktop.Views
{
    /// <summary>
    ///     Interaktionslogik für BaseValueView.xaml
    /// </summary>
    public partial class BaseValueView
    {
        private BaseValue _baseValue;

        public BaseValueView(BaseValue baseValue)
        {
            BaseValue = baseValue;

            DataContext = baseValue;

            InitializeComponent();
        }

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
    }
}