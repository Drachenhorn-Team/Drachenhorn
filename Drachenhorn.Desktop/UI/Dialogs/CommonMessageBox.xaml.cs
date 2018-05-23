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

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für CommonMessageBox.xaml
    /// </summary>
    public partial class CommonMessageBox : Window
    {
        public CommonMessageBox(string message, string title, string buttonConfirmText, string buttonCancelText)
        {
            InitializeComponent();

            this.Title = title;
            MessageBlock.Text = message;
            ConfirmButton.Content = buttonConfirmText;
            CancelButton.Content = buttonCancelText;
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
