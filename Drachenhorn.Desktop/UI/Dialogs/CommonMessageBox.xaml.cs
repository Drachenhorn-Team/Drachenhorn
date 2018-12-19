using System.Windows;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    ///     Interaktionslogik für CommonMessageBox.xaml
    /// </summary>
    public partial class CommonMessageBox
    {
        #region c'tor

        public CommonMessageBox(string message, string title, string buttonConfirmText, string buttonCancelText = null)
        {
            InitializeComponent();

            Title = title;
            MessageBlock.Text = message;
            ConfirmButton.Content = buttonConfirmText;

            if (string.IsNullOrEmpty(buttonCancelText))
                CancelButton.Visibility = Visibility.Collapsed;
            else
                CancelButton.Content = buttonCancelText;
        }

        #endregion

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