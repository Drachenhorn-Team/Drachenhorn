using Drachenhorn.Xml.Interfaces;

namespace Drachenhorn.Desktop.Views
{
    /// <summary>
    ///     Interaktionslogik für InfoView.xaml
    /// </summary>
    public partial class InfoView
    {
        public InfoView(IInfoObject infoObject)
        {
            InitializeComponent();

            Resources["Information"] = infoObject.GetInformation();
        }
    }
}