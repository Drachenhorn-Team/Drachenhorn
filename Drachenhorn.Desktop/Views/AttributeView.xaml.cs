using System.Windows;
using Drachenhorn.Xml.Sheet.Skills;

namespace Drachenhorn.Desktop.Views
{
    /// <summary>
    ///     Interaktionslogik für AttributeView.xaml
    /// </summary>
    public partial class AttributeView
    {
        private Attribute _attribute;

        public AttributeView(Attribute attribute)
        {
            Attribute = attribute;

            DataContext = Attribute;

            InitializeComponent();
        }

        public Attribute Attribute
        {
            get => _attribute;
            private set
            {
                if (_attribute == value)
                    return;
                _attribute = value;
            }
        }
    }
}