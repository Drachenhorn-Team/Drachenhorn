using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DSACharacterSheet.Desktop.Dialogs;
using GalaSoft.MvvmLight.Views;

namespace DSACharacterSheet.Desktop.MVVM
{
    public class DialogService : IDialogService
    {
        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() => { new ExceptionMessageBox(error, title).ShowDialog(); });
        }

        public Task ShowMessage(string message, string title)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public async Task ShowMessageBox(string message, string title)
        {
            await Task.Run(() => { MessageBox.Show(message, title, MessageBoxButton.OK); });
        }
    }
}
