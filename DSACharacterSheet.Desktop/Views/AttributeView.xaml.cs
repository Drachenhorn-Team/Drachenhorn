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
using DSACharacterSheet.FileReader.Sheet.Skills;

namespace DSACharacterSheet.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für AttributeView.xaml
    /// </summary>
    public partial class AttributeView : Window
    {
        private Attribute _attribute;

        public Attribute Attribute
        {
            get { return _attribute; }
            private set
            {
                if (_attribute == value)
                    return;
                _attribute = value;
            }
        }

        public AttributeView(Attribute attribute)
        {
            Attribute = attribute;

            DataContext = Attribute;

            InitializeComponent();
        }
    }
}
