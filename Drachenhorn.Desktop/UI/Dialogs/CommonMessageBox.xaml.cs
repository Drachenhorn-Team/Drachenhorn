using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    ///     Interaktionslogik für CommonMessageBox.xaml
    /// </summary>
    public partial class CommonMessageBox
    {
        private int _result = -1;

        #region c'tor

        public CommonMessageBox(string message, string title, string[] buttons)
        {
            InitializeComponent();

            Title = title;
            MessageBlock.Text = message;
            ButtonControl.ItemsSource = buttons;
        }

        #endregion

        private void DialogButton_OnClick(object sender, RoutedEventArgs e)
        {
            var buttons = (string[]) ButtonControl.ItemsSource;
            var clickText = ((Button) sender).Content.ToString();

            _result = Array.FindIndex(buttons, x => x == clickText);

            DialogResult = true;
            Close();
        }

        public new int ShowDialog()
        {
            var result = base.ShowDialog() == true;

            if (result && _result != -1)
                return _result;

            return -1;
        }
    }
}