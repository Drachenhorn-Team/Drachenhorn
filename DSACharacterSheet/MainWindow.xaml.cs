using System;
using System.Collections.Generic;
using System.Deployment.Application;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DSACharacterSheet.DataObjects;
using DSACharacterSheet.Dialogs;

namespace DSACharacterSheet
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CharacterSheet CurrentSheet = new CharacterSheet();


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = CurrentSheet;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.IsFirstRun)
                new ChangeLogWindow().ShowDialog();
        }
    }
}
