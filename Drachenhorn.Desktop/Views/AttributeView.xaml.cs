using Drachenhorn.Xml.Sheet.Skills;

namespace Drachenhorn.Desktop.Views
{
    /// <summary>
    ///     Interaktionslogik für AttributeView.xaml
    /// </summary>
    public partial class AttributeView
    {
        #region c'tor

        public AttributeView(Attribute attribute)
        {
            Attribute = attribute;

            DataContext = Attribute;

            InitializeComponent();
        }

        #endregion

        #region Properties

        private Attribute _attribute;

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

        #endregion
    }
}