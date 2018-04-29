using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DSACharacterSheet.Xml.Sheet.Skills;

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
