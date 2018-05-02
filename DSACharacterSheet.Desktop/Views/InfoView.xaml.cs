using DSACharacterSheet.Xml.Interfaces;
using System.Windows;

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