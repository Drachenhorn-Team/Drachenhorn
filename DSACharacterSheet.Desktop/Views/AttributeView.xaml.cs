using DSACharacterSheet.Xml.Sheet.Skills;
using System.Windows;

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