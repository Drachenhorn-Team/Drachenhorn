using System.Windows;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    ///     Interaktionslogik für CommonMessageBox.xaml
    /// </summary>
    public partial class CommonMessageBox : Window
    {
        public CommonMessageBox(string message, string title, string buttonConfirmText, string buttonCancelText)
        {
            InitializeComponent();

            Title = title;
            MessageBlock.Text = message;
            ConfirmButton.Content = buttonConfirmText;
            CancelButton.Content = buttonCancelText;
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}