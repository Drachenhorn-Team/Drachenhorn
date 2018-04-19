using System.Windows;
using DSACharacterSheet.Xml.Interfaces;

namespace DSACharacterSheet.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für InfoView.xaml
    /// </summary>
    public partial class InfoView : Window
    {
        public InfoView(IInfoObject infoObject)
        {
            InitializeComponent();

            Resources["Information"] = infoObject.GetInformation();
        }
    }
}